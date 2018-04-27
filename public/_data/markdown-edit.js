(function ($el) {
  var tpl = `
    <div class="btn-toolbar mdedit">
      <div class="btn-group ml-auto">
        <button class="btn btn-secondary mdedit-render" type="button">
          미리보기
        </button>
        <button class="btn btn-secondary mdedit-help" type="button">
          작성 방법
        </button>
      </div>
    </div>
  `;

  $(document).on('click', '.mdedit-render', function (e) {
    var form = document.createElement('form');
    form.hidden = true;
    form.method = 'post';;
    form.action = '/pro/markdown/render';

    // setting form target to a window named 'formresult'
    form.target = 'formresult';

    form.appendChild($('<textarea name="content">').val($(e.target).closest('.mdedit').next().val())[0]);

    document.body.appendChild(form);

    // creating the 'formresult' window with custom features prior to submitting the form
    window.open(null, 'formresult', 'menubar=no,height=600,width=800,resizable=yes,toolbar=no,status=no');

    form.submit();
  });
  $(document).on('click', '.mdedit-help', function (e) {
    window.open('http://parsedown.org/tests/');
  });

  $el.before(tpl);
})($('.markdown-edit'));
