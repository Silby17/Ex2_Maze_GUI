using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    
    public interface IPresenter
    {
        void OnEventHandler(object source, EventArgs e);
        void HandleViewEvent(object src);
        void HandleModelEvent(object src);
    }
}
