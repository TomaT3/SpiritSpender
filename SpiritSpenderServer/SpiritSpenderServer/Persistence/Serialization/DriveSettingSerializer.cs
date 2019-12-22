//using MongoDB.Bson.Serialization;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Threading.Tasks;

//namespace SpiritSpenderServer.Persistence.Serialization
//{
//    public class DriveSettingSerializer : IBsonSerializer
//    {
//        public Type ValueType => throw new NotImplementedException();

//        public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
//        {
//            context.
//            throw new NotImplementedException();
//        }

//        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
//        {
//            if (value == null)
//            {
//                context.Writer.WriteNull();
//            }
//            else
//            {
                
//                List<MemberInfo> members = base.GetAllSerializableMembers(ValueType);

//                context.Writer.WriteStartDocument();
//                foreach (MemberInfo member in members)
//                {
//                    context.Writer.WriteName(member.Name);
//                    BsonSerializer.Serialize(context.Writer, Reflection.Info.GetMemberType(member), Reflection.Info.GetMemberValue(member, value), null, args);
//                }
//                context.Writer.WriteEndDocument();
//            }
//        }
//    }
//}
