using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace NHLPlayers
{
    public class SortableList<T> : BindingList<T>
    {
        protected override bool SupportsSortingCore => true;
        
        public SortableList(IList<T> list) : base(list)
        {
        }
        
        // Rewrite the ApplySortCore method to sort the list based on the property types
        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            List<T> list = this.ToList();

            if (prop.PropertyType == typeof(double))
            {
                list.Sort((x ,y) => direction == ListSortDirection.Ascending ?
                    Comparer<double>.Default.Compare((double)prop.GetValue(x), (double)prop.GetValue(y)) :
                    Comparer<double>.Default.Compare((double)prop.GetValue(y), (double)prop.GetValue(x)));
            }
            else if (prop.PropertyType == typeof(string))
            {
                list.Sort((x, y) => direction == ListSortDirection.Ascending ?
                    Comparer<string>.Default.Compare((string)prop.GetValue(x), (string)prop.GetValue(y)) :
                    Comparer<string>.Default.Compare((string)prop.GetValue(y), (string)prop.GetValue(x)));
            }
            else
            {
                list.Sort((x, y) => direction == ListSortDirection.Ascending ?
                    Comparer<object>.Default.Compare(prop.GetValue(x), prop.GetValue(y)) :
                    Comparer<object>.Default.Compare(prop.GetValue(y), prop.GetValue(x)));
            }
            
            RaiseListChangedEvents = false;
            ClearItems();

            foreach (var item in list) Add(item);
            
            RaiseListChangedEvents = true;
            ResetBindings();
        }
    }
}