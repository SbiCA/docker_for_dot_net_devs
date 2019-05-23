    public class ClassNameSerializationBinder : DefaultSerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            var localType = Assembly.GetExecutingAssembly().GetTypes().SingleOrDefault(t => typeName.EndsWith($".{t.Name}"));
            return localType ?? base.BindToType(assemblyName, typeName);
        }
    }
    
        .Serialization(s => s.UseNewtonsoftJson(new JsonSerializerSettings(){SerializationBinder = new ClassNameSerializationBinder()}));
