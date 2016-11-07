using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Items
{
    class Resource : Item, IResource
    {
        public string ResourceType { get; set; }

        public Resource()
        {

        }

        public Resource(Resource item)
        {
            // TODO: Complete member initialization
            ResourceType = item.ResourceType;
            CopyData(item);
        }
        
    }
}
