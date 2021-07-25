using StorefrontModels;
using System.Collections.Generic;
using System;

namespace StorefrontDL{

    public interface IStoreRepository{
        //Gets all stores and lists them out
        List<Storefront> GetAllStores();
        Storefront GetStorefront(int id);

        Storefront AddStore(Storefront store);
        LineItem Replenish(LineItem item);
    }
}

