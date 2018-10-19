function createCommentItem(commentForm, path) {
    var service = new ItemService({ url: '/sitecore/api/ssc/item' });
    var obj = {
        ItemName: 'comment-' + commentForm.email.value,
        TemplateID: '{EEE7D1DC-275E-4399-BDA5-A0F3128EDEE0}',
        Name: commentForm.email.value,
        Comment: commentForm.comment.value
    };

    service.create(obj)
        .path(path)
        .execute()
        .then(function(item) {
            commentForm.email.value = commentForm.comment.value = '';
            window.alert('Thanks. Your message will show on the site shortly!');
        })
        .fail(function (err) { window.alert(err); });
    event.preventDefault();
    return false;

}
