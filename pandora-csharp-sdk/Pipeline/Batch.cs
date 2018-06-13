using System.Collections.Generic;
using System.Text;

namespace Qiniu.Pandora.Pipeline
{
    public class Batch
    {
        public const int MaxBatchSize = 2 * 1024 * 1024;
        public List<Point> points = new List<Point>();
        private int maxSize = MaxBatchSize;
        private int size;
        public List<Point> GetPoints()
        {
            return this.points;
        }

        public void Add(Point p)
        {
            this.points.Add(p);
            this.size += p.GetSize();
        }

        public int GetSize()
        {
            return this.size;
        }

        public void SetMaxSize(int maxSize)
        {
            this.maxSize = maxSize;
        }

        public bool IsFull()
        {
            return this.size > this.maxSize;
        }

        public bool CanAdd(Point p)
        {
            return this.size + p.GetSize() <= this.maxSize;
        }

        public void Clear()
        {
            this.points.Clear();
            this.size = 0;
        }

        public override string ToString()
        {
            List<string> pointsStr = new List<string>();
            foreach (Point p in this.points)
            {
                pointsStr.Add(p.ToString());
            }
            return string.Join("", pointsStr);
        }
    }
}
