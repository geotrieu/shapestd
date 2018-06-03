using System.Collections;

namespace ShapesTD
{
    public class BasePair
    {
        private BaseTower bt;
        private BaseEnemy be;
            
        public BasePair(BaseTower bt, BaseEnemy be)
        {
            this.bt = bt;
            this.be = be;
        }

        public BaseTower GetTower()
        {
            return bt;
        }
            
        public BaseEnemy GetEnemy()
        {
            return be;
        }

        public static BasePair FindBasePair(ArrayList al, BaseTower bt, BaseEnemy be)
        {
            foreach (BasePair bp in al)
            {
                if (bp.GetTower() == bt && bp.GetEnemy() == be)
                    return bp;
            }

            return null;
        }

        public static ArrayList FindBasePair(ArrayList al, BaseEnemy be)
        {
            ArrayList result = new ArrayList();
            foreach (BasePair bp in al)
            {
                if (bp.GetEnemy() == be)
                    result.Add(bp);
            }

            return result;
        }
    }
}