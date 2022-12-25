using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace DO
{
    [Serializable]
    public class AlreadyExistException : Exception, ISerializable
    {
        public AlreadyExistException() : base() { }
        public AlreadyExistException(string message) : base(message) { }
        public AlreadyExistException(string message, Exception inner) : base(message, inner) { }
        protected AlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        override public string ToString() =>
       "The value Already Exist in List!!";
    }
    [Serializable]
    public class DoesntExistException : Exception, ISerializable
    {
        public DoesntExistException() : base() { }
        public DoesntExistException(string message) : base(message) { }
        public DoesntExistException(string message, Exception inner) : base(message, inner) { }
        protected DoesntExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        override public string ToString() =>
       "The Value is Not Exist in List!!";
    }
    [Serializable]
    public class notExistElementInList : Exception, ISerializable
    {
        public notExistElementInList() : base() { }
        public notExistElementInList(string message) : base(message) { }
        public notExistElementInList(string message, Exception inner) : base(message, inner) { }
        protected notExistElementInList(SerializationInfo info, StreamingContext context) : base(info, context) { }
        override public string ToString() =>
       "The list is Empty!! ";
    }

    [Serializable]
    public class DalConfigException : Exception
    {
        public DalConfigException(string msg) : base(msg) { }
        public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
    }

}
