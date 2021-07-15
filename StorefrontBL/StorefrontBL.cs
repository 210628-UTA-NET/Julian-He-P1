  
using System.Collections.Generic;
using StorefrontDL;
using StorefrontModels;

namespace StorefrontBL
{
    public class StoreBL : IStoreBL
    {
        /// <summary>
        /// We are defining the dependenices this class needs in the constructor
        /// We do it this way (with interfaces) because we can easily switch out the implementation of RRDL when we want to change data source 
        /// (change from file system into database stored in the cloud)
        /// </summary>
        private IStoreRepository _repo;
        public StoreBL(IStoreRepository p_repo)
        {
            _repo = p_repo;
        }

        public Storefront AddStore(Storefront p_store)
        {
            return _repo.AddStore(p_store);
        }

        public List<Storefront> GetAllStore()
        {
            return _repo.GetAllStores();
        }
        public Storefront GetStorefront(int p_store){
            return _repo.GetStorefront(p_store);
        }
        public LineItem Replenish(LineItem item, int amt){
                return _repo.Replenish( item, amt);
        }

    }
}