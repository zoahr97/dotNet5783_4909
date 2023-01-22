using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

public static class InstanceXml
{
    public static IDal Instance = new DalXml();
    public static IDal Get1()
    {
        return Instance;
    }
}

