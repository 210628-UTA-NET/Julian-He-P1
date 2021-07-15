using System.Collections.Generic;
using StorefrontModels;
using StorefrontDL;

namespace StorefrontBL{
    public class LineItemBL : ILineItemBL
    {

        private ILineItemRepository _repo;
        public LineItemBL(LineItemRepository p_repo)
        {
            _repo = p_repo;
        }
        public LineItem AddLineItem(LineItem p_lineitem)
        {
            _repo.AddLineItem(p_lineitem);
            return p_lineitem;
        }

        public List<LineItem> GetAllLineItem()
        {
            return _repo.GetAllLineItems();
        }

        public List<LineItem> GetLineItem(string param, int i)
        {
            return _repo.GetLineItem(param, i);
        }

        public LineItem UpdateLineItem(LineItem p_lineitem, int amt)
        {
            return _repo.UpdateLineItem(p_lineitem, amt);
        }

        public List<LineItem> GetInventory(int id){
            return _repo.GetInventory(id);
        }
    }
}