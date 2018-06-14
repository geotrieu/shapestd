/*****************************************************
 * Name: George Trieu
 * Date: 2018-06-05
 * Title: BasePair
 * Purpose: This object comprises of a BaseTower and a
 *          BaseEnemy, and is primairly used for storing
 *          which enemy is getting shot by which tower.
 ****************************************************/
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

        /*****************************************************
        * Name: George Trieu
        * Date: 2018-06-08
        * Title: FindBasePair (ArrayList, BaseTower, BaseEnemy)
        * Purpose: Find the BasePair that contains both the BaseTower
        *          supplied and the BaseEnemy supplied
        * Inputs: ArrayList al
        *         BaseTower bt
        *         BaseEnemy be
        * Returns: One BasePair that contains the BaseTower,
        *          and BaseEnemy from the supplied ArrayList,
        *          or else it returns null.
        ****************************************************/
        public static BasePair FindBasePair(ArrayList al, BaseTower bt, BaseEnemy be)
        {
            foreach (BasePair bp in al)
            {
                if (bp.GetTower() == bt && bp.GetEnemy() == be)
                    return bp;
            }

            return null;
        }

        /*****************************************************
        * Name: George Trieu
        * Date: 2018-06-08
        * Title: FindBasePair (ArrayList, BaseEnemy)
        * Purpose: Find all the BasePairs that contains the BaseEnemy
        *          supplied.
        * Inputs: ArrayList al
        *         BaseEnemy be
        * Returns: All BasePairs that contains the BaseEnemy,
        *          from the supplied ArrayList, returns an ArrayList
        *          of BasePairs.
        ****************************************************/
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
        
        /*****************************************************
        * Name: George Trieu
        * Date: 2018-06-08
        * Title: FindBasePair (ArrayList, BaseTower)
        * Purpose: Find all the BasePairs that contains the BaseTower
        *          supplied.
        * Inputs: ArrayList al
        *         BaseTower bt
        * Returns: All BasePairs that contains the BaseTower,
        *          from the supplied ArrayList, returns an ArrayList
        *          of BasePairs.
        ****************************************************/
        public static ArrayList FindBasePair(ArrayList al, BaseTower bt)
        {
            ArrayList result = new ArrayList();
            foreach (BasePair bp in al)
            {
                if (bp.GetTower() == bt)
                    result.Add(bp);
            }

            return result;
        }
    }
}