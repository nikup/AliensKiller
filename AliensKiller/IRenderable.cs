using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliensKiller
{
    public interface IRenderable
    {
        MatrixCoordinates GetTopLeft();

        char[,] GetImage();
    }
}
