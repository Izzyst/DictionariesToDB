using System;
using System.Collections.Generic;
using Android.Widget;

namespace App3.FilePicker
{
    public class FileListRowViewHolder : Object
    {
        public FileListRowViewHolder(TextView textView, ImageView imageView)
        {
            TextView = textView;
            ImageView = imageView;
        }

        public ImageView ImageView { get; private set; }
        public TextView TextView { get; private set; }

        /// <summary>
        ///   This method will update the TextView and the ImageView that are
        ///   are
        /// </summary>
        /// <param name="fileName"> </param>
        /// <param name="fileImageResourceId"> </param>
        public void Update(string fileName, int fileImageResourceId)
        {
            TextView.Text = fileName;
            ImageView.SetImageResource(fileImageResourceId);
        }
    }
}