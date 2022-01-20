(function () {

    // File upload
    function handleFileSelect(event) {
        if (window.File && window.FileList && window.FileReader) {

            var files = event.target.files;

            for (var i = 0; i < files.length; i++) {
                var file = files[i];

                // Only image uploads supported
                if (!file.type.match('image'))
                    continue;

                var reader = new FileReader();
                reader.addEventListener("load", function (event) {
                    var image = new Image();
                    image.alt = file.name;
                    image.onload = function (e) {
                        image.setAttribute("data-filename", file.name);
                        image.setAttribute("width", image.width.toString());
                        image.setAttribute("height", image.height.toString());
                        window.tinymce.activeEditor.execCommand('mceInsertContent', false, image.outerHTML);
                    };
                    image.src = this.result;

                });

                reader.readAsDataURL(file);
            }

            document.body.removeChild(event.target);
        }
        else {
            console.log("Your browser does not support File API");
        }
    }

    // remove empty strings
    function removeEmpty(item) {
        var trimmedItem = item.trim();
        if (trimmedItem.length > 0) {
            return trimmedItem;
        }
    }

    // edit form
    var edit = document.getElementById("edit");
    // Setup editor
    var editPost = document.getElementById("content");

    if (edit && editPost) {

        if (typeof window.orientation !== "undefined" || navigator.userAgent.indexOf('IEMobile') !== -1) {
            window.tinymce.init({
                selector: '#content',
                theme: 'modern',
                mobile: {
                    theme: 'mobile',
                    plugins: ['autosave', 'lists', 'autolink'],
                    toolbar: ['undo', 'bold', 'italic', 'styleselect']
                }
            });
        }
        else {
            window.tinymce.init({
                selector: '#content',
                min_height: 500,
                autoresize_min_height: 200,
                plugins: 'autosave preview searchreplace visualchars image link media fullscreen code codesample table hr pagebreak autoresize nonbreaking anchor insertdatetime advlist lists textcolor wordcount imagetools colorpicker',
                menubar: "edit view format insert table",
                toolbar1: 'formatselect | bold italic blockquote forecolor backcolor | imageupload link | alignleft aligncenter alignright  | numlist bullist outdent indent | fullscreen',
                selection_toolbar: 'bold italic | quicklink h2 h3 blockquote',
                autoresize_bottom_margin: 0,
                paste_data_images: true,
                image_advtab: true,
                file_picker_types: 'image',
                relative_urls: false,
                convert_urls: false,
                branding: false,
                /* and here's our custom image picker*/
                file_picker_callback: function (cb, value, meta) {
                    var input = document.createElement('input');
                    input.setAttribute('type', 'file');
                    input.setAttribute('accept', 'image/*');

                    /*
                      Note: In modern browsers input[type="file"] is functional without
                      even adding it to the DOM, but that might not be the case in some older
                      or quirky browsers like IE, so you might want to add it to the DOM
                      just in case, and visually hide it. And do not forget do remove it
                      once you do not need it anymore.
                    */

                    input.onchange = function () {
                        var file = this.files[0];

                        var reader = new FileReader();
                        reader.onload = function () {
                            /*
                              Note: Now we need to register the blob in TinyMCEs image blob
                              registry. In the next release this part hopefully won't be
                              necessary, as we are looking to handle it internally.
                            */
                            var id = 'blobid' + (new Date()).getTime();
                            var blobCache = tinymce.activeEditor.editorUpload.blobCache;
                            var base64 = reader.result.split(',')[1];
                            var blobInfo = blobCache.create(id, file, base64);
                            blobCache.add(blobInfo);

                            /* call the callback and populate the Title field with the file name */
                            cb(blobInfo.blobUri(), { title: file.name });
                        };
                        reader.readAsDataURL(file);
                    };

                    input.click();
                },
                content_style: 'body { font-family:Helvetica,Arial,sans-serif; font-size:14px }'
            });
        }

        // Delete post
        var deleteButton = edit.querySelector(".delete");
        if (deleteButton) {
            deleteButton.addEventListener("click", function (e) {
                if (!confirm("Are you sure you want to delete the post?")) {
                    e.preventDefault();
                }
            });
        }
    }

    // Delete comments
    var deleteLinks = document.querySelectorAll("#comments a.delete");
    if (deleteLinks) {
        for (var i = 0; i < deleteLinks.length; i++) {
            var link = deleteLinks[i];

            link.addEventListener("click", function (e) {
                if (!confirm("Are you sure you want to delete the comment?")) {
                    e.preventDefault();
                }
            });
        }
    }

    // Category input enhancement - using autocomplete input
    var selectcat = document.getElementById("selectcat");
    var categories = document.getElementById("categories");
    if (selectcat && categories) {

        selectcat.onchange = function () {

            var phv = selectcat.placeholder;
            var val = selectcat.value.toLowerCase();

            var phv_array = phv.split(",").map(function (item) {
                return removeEmpty(item);
            });

            var val_array = val.split(",").map(function (item) {
                return removeEmpty(item);
            });

            for (var j = val_array.length - 1; j >= 0; j--) {
                var v = val_array[j];
                var flag = false;
                for (var i = phv_array.length - 1; i >= 0; i--) {
                    if (phv_array[i] === v) {
                        phv_array.splice(i, 1);
                        flag = true;
                    }
                }
                if (!flag) {
                    phv_array.push(v);
                }
            }

            selectcat.placeholder = phv_array.join(", ");
            categories.value = selectcat.placeholder;
            selectcat.value = "";
        };
    }

    // Tag input enhancement - using autocomplete input
    var selecttag = document.getElementById("selecttag");
    var tags = document.getElementById("tags");
    if (selecttag && tags) {

        selecttag.onchange = function () {

            var phv = selecttag.placeholder;
            var val = selecttag.value.toLowerCase();

            var phv_array = phv.split(",").map(function (item) {
                return removeEmpty(item);
            });

            var val_array = val.split(",").map(function (item) {
                return removeEmpty(item);
            });

            for (var j = val_array.length - 1; j >= 0; j--) {
                var v = val_array[j];
                var flag = false;
                for (var i = phv_array.length - 1; i >= 0; i--) {
                    if (phv_array[i] === v) {
                        phv_array.splice(i, 1);
                        flag = true;
                    }
                }
                if (!flag) {
                    phv_array.push(v);
                }
            }

            selecttag.placeholder = phv_array.join(", ");
            tags.value = selecttag.placeholder;
            selecttag.value = "";
        };
    }

})();