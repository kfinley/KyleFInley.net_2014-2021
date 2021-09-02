using _928.Entities;

namespace _928.Web.Mvc {
    
    public class ViewData<T> : BaseViewData<T> 
        where T : Entity {

        public ViewData()
            : base() {

        }

        public override bool NoIndex {
            get {
                return !this.Page.Enabled;
            }
        }
    }
}
