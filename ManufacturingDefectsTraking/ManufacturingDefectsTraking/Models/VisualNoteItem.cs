using ManufacturingDefectsTraking.Enums;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ManufacturingDefectsTraking.Models
{
    public class VisualNoteItem
    {
       [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public ImageSource ItemImageSource
        {
            get
            {
                if (Picture != null)
                    return ImageSource.FromFile(Picture);
                else
                    return null;
            }           
        }
        public int Status { get; set; }
        public string DisplayingStatus
        {
            get { return Status == (int)StatusEnum.Open ? StatusEnum.Open.ToString() : StatusEnum.Closed.ToString(); }
        }
        public DateTime Date { get; set; }
    }
}
