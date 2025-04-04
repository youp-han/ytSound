using System.Drawing;
using System;
using System.Windows.Forms;
using Google.Apis.YouTube.v3.Data;

namespace ytSound.utility
{
    public class ListViewWithButtons : ListView
    {

        public ListViewWithButtons()
        {
            this.View = View.Details;
            this.FullRowSelect = true;
            this.CheckBoxes = true;
            this.MouseClick += ListViewWithButtons_MouseClick;
        }

        private void  ListViewWithButtons_MouseClick(object sender, MouseEventArgs e)
        {
            var hitTest = this.HitTest(e.Location);
            string thumbnailUrl = string.Empty;

            if (hitTest.Item != null && hitTest.SubItem != null)
            {
                // Get the thumbnail URL from the specified sub-item index (assuming index 7)
                thumbnailUrl = hitTest.Item.SubItems[7].Text;

                // Call ShowImageInPictureBox with the retrieved URL
                ShowImageInPictureBox(thumbnailUrl);
            }
        }
        

        private void ShowImageInPictureBox(string imageUrl)
        {
            try
            {
                // PictureBox의 Name 속성을 기준으로 기존 PictureBox 검색
                PictureBox pictureBox = this.Parent.Controls["imageBox"] as PictureBox;

                if (pictureBox != null)
                {
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage; // 크기 조정
                    pictureBox.Load(imageUrl); // 이미지 로드
                }
                else
                {
                    MessageBox.Show("PictureBox가 존재하지 않습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"이미지 로드 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
