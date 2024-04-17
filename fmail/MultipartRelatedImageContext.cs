﻿using MimeKit.Text;
using MimeKit;
using System;
using System.IO;

namespace fmail
{
    /// <summary>
    /// Represents the context for handling multipart/related MIME entities in HTML.
    /// </summary>
    class MultipartRelatedImageContext
    {
        readonly MultipartRelated related;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultipartRelatedImageContext"/> class.
        /// </summary>
        /// <param name="related">The multipart/related MIME entity.</param>
        public MultipartRelatedImageContext(MultipartRelated related)
        {
            this.related = related;
        }

        /// <summary>
        /// Gets the data URI for a MIME part.
        /// </summary>
        /// <param name="attachment">The MIME part representing an attachment.</param>
        /// <returns>The data URI for the attachment.</returns>
        string GetDataUri(MimePart attachment)
        {
            using (var memory = new MemoryStream())
            {
                attachment.Content.DecodeTo(memory);
                var buffer = memory.GetBuffer();
                var length = (int)memory.Length;
                var base64 = Convert.ToBase64String(buffer, 0, length);

                return string.Format("data:{0};base64,{1}", attachment.ContentType.MimeType, base64);
            }
        }

        /// <summary>
        /// Callback method for handling HTML tags.
        /// </summary>
        /// <param name="ctx">The HTML tag context.</param>
        /// <param name="htmlWriter">The HTML writer.</param>
        public void HtmlTagCallback(HtmlTagContext ctx, HtmlWriter htmlWriter)
        {
            if (ctx.TagId != HtmlTagId.Image || ctx.IsEndTag)
            {
                ctx.WriteTag(htmlWriter, true);
                return;
            }

            // write the IMG tag, but don't write out the attributes.
            ctx.WriteTag(htmlWriter, false);

            // manually write the attributes so that we can replace the SRC attributes
            foreach (var attribute in ctx.Attributes)
            {
                if (attribute.Id == HtmlAttributeId.Src)
                {
                    int index;
                    Uri uri;

                    // parse the <img src=...> attribute value into a Uri
                    if (Uri.IsWellFormedUriString(attribute.Value, UriKind.Absolute))
                        uri = new Uri(attribute.Value, UriKind.Absolute);
                    else
                        uri = new Uri(attribute.Value, UriKind.Relative);

                    // locate the index of the attachment within the multipart/related (if it exists)
                    if ((index = related.IndexOf(uri)) != -1)
                    {
                        if (!(related[index] is MimePart attachment))
                        {
                            // the body part is not a basic leaf part (IOW it's a multipart or message-part)
                            htmlWriter.WriteAttribute(attribute);
                            continue;
                        }

                        var data = GetDataUri(attachment);

                        htmlWriter.WriteAttributeName(attribute.Name);
                        htmlWriter.WriteAttributeValue(data);
                    }
                    else
                    {
                        htmlWriter.WriteAttribute(attribute);
                    }
                }
            }
        }
    }
}
