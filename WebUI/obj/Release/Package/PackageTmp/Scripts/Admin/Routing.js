
$(".SpaceSubMenu").click(function () {
    $('#imgSep1').show();
          switch (this.parentElement.id)
          {
              case 'customer':
                  {
                      $('#PathMenu').text('گالری');
                      break;
                  }
              case 'settings':
                  {
                      $('#PathMenu').text('تنظیمات');
                      break;
                  }
              case 'users':
                  {
                      $('#PathMenu').text('اخبار');
                      break;
                  }
                  

    }
         // $('#LinkPathSubMenu').text($(this).text());
    // $('#LinkPathSubMenu').text('test');
    // $('#LinkPathSubMenu').attr("href", $('a', this).filter("[href]").attr('href'));
    //  alert($('#LinkPathSubMenu').filter("[href]").attr('href'));
         // $('#HeaderTitleP').text($(this).text());
         
      });

