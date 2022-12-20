using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace BO
{
    [Serializable]
    public class AlreadyExistException : Exception, ISerializable
    {
        public AlreadyExistException() : base() { }
        public AlreadyExistException(string message) : base(message) { }
        public AlreadyExistException(string message, Exception inner) : base(message, inner) { }
        protected AlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class DoesntExistException : Exception, ISerializable
    {
        public DoesntExistException() : base() { }
        public DoesntExistException(string message) : base(message) { }
        public DoesntExistException(string message, Exception inner) : base(message, inner) { }
        protected DoesntExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
    [Serializable]
    public class RequestFailed : Exception, ISerializable
    {
        public RequestFailed() : base() { }
        public RequestFailed(string message) : base(message) { }
        public RequestFailed(string message, Exception inner) : base(message, inner) { }
        protected RequestFailed(SerializationInfo info, StreamingContext context) : base(info, context) { }
        override public string ToString() =>
       "The Request is Failed!!";
    }
    [Serializable]
    public class notExistElementInList : Exception, ISerializable
    {
        public notExistElementInList() : base() { }
        public notExistElementInList(string message) : base(message) { }
        public notExistElementInList(string message, Exception inner) : base(message, inner) { }
        protected notExistElementInList(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    //[Serializable]
    //public class UnfoundException : Exception, ISerializable
    //{
    //    public UnfoundException() : base() { }
    //    public UnfoundException(string message) : base(message) { }
    //    public UnfoundException(string message, Exception inner) : base(message, inner) { }
    //    protected UnfoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    //}

}
