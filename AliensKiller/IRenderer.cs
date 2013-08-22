using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliensKiller
{
    public interface IRenderer
    {
        void EnqueueForRendering(IRenderable obj);

        void RenderAll();

        void ClearQueue();
    }
}
