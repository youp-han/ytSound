using System.Drawing;
using System;
using System.Windows.Forms;

namespace ytSound.utility
{
    public class ListViewWithButtons : ListView
    {

        public ListViewWithButtons()
        {
            this.View = View.Details;
            this.FullRowSelect = true;
            this.CheckBoxes = true;
        }


        public void AddControlToItem(Control control, int itemIndex, int subItemIndex)
        {
            if (itemIndex < 0 || itemIndex >= this.Items.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(itemIndex), "Item index is out of range.");
            }

            if (subItemIndex < 0 || subItemIndex >= this.Items[itemIndex].SubItems.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(subItemIndex), "Sub-item index is out of range.");
            }

            var bounds = this.Items[itemIndex].SubItems[subItemIndex].Bounds;

            // Adjust the control's location and size to fit within the bounds of the cell
            control.SetBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height);

            // Bring the control to the front to make sure it's visible
            this.Controls.Add(control);
            control.BringToFront();


        }

        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            base.OnDrawSubItem(e);

        }
    }
}
