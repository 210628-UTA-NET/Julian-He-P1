using StorefrontModels;
using System.Collections.Generic;
using System;
namespace StorefrontDL{
    public interface ILineItemRepository{

        ///returns all customers in repo
        List<LineItem> GetAllLineItems();
        List<LineItem> GetLineItem(string param, int i);
        LineItem AddLineItem(LineItem lineitem);
        LineItem UpdateLineItem(LineItem lineitem, int amt);

        List<LineItem> GetInventory(int i);
    }
}