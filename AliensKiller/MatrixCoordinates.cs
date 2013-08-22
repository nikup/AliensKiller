using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliensKiller
{
   public  class MatrixCoordinates
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public MatrixCoordinates(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        public static MatrixCoordinates operator + (MatrixCoordinates a, MatrixCoordinates b)
        {
            return new MatrixCoordinates(a.Row + b.Row, a.Col + b.Col);
        }

        public static MatrixCoordinates operator -(MatrixCoordinates a, MatrixCoordinates b)
        {
            return new MatrixCoordinates(a.Row - b.Row, a.Col - b.Col);
        }

        public override bool Equals(object obj)
        {
            MatrixCoordinates objAsMatrixCoordinates = obj as MatrixCoordinates;

            return objAsMatrixCoordinates.Row == this.Row && objAsMatrixCoordinates.Col == this.Col;
        }

        public override int GetHashCode()
        {
            return this.Row.GetHashCode()*7 + this.Col;
        }
    }
}
