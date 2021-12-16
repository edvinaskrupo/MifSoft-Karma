using AspectInjector.Broker;
using Castle.DynamicProxy;
using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace Care.Helpers
{
    [Aspect(Scope.Global)]
    [Injection(typeof(LogCall))]
    public class LogCall : Attribute
    {
        [Advice(Kind.Before)] // you can have also After (async-aware), and Around(Wrap/Instead) kinds
        public void LogEnter([Argument(Source.Name)] string name)
        {
            Debug.WriteLine($"Calling '{name}' method...");   //you can debug it	
        }
    }
}
