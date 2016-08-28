using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasker.Model;
using Newtonsoft.Json;

namespace Tasker.Infra
{
    public class TodoRepository : ITodoRepository
    {
        private static ConcurrentDictionary<string, TodoItem> _todos = new ConcurrentDictionary<string, TodoItem>();

        private static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");

        private const string prefix = "todo:";

        public void Add(TodoItem item)
        {
            item.Key = Guid.NewGuid().ToString();
            item.Register = DateTime.Now;
            string json = JsonConvert.SerializeObject(item);

            IDatabase db = redis.GetDatabase();
            db.StringSet(prefix + item.Key, json);
        }

        public TodoItem Find(string key)
        {
            TodoItem item;

            IDatabase db = redis.GetDatabase();
            string jsonItem = db.StringGet(prefix + key);
            if (string.IsNullOrEmpty(jsonItem))
            {
                return null;
            }

            item = JsonConvert.DeserializeObject<TodoItem>(jsonItem);
            return item;
        }

        public IEnumerable<TodoItem> GetAll()
        {
            var server = redis.GetServer("localhost:6379");
            var allKeys = server.Keys(0, prefix + "*");

            IDatabase db = redis.GetDatabase();
            var itens = db.StringGet(allKeys.ToArray());

            var ret = new List<TodoItem>();
            foreach (var item in itens)
            {
                ret.Add(JsonConvert.DeserializeObject<TodoItem>(item));
            }

            return ret;
        }

        public TodoItem Remove(string key)
        {
            IDatabase db = redis.GetDatabase();

            var json = db.StringGet(prefix + key);

            db.KeyDelete(prefix + key);

            return JsonConvert.DeserializeObject<TodoItem>(json);
        }

        public void Update(TodoItem item)
        {
            string json = JsonConvert.SerializeObject(item);

            IDatabase db = redis.GetDatabase();
            db.StringSet(prefix + item.Key, json);
        }
    }
}
