$('.modal.pet').on('swow.bs.modal', function () {
  $('body').css('top','-100vh');
});

$('.modal.pet').on('hide.bs.modal', function () {
  $('body').css('top','0');
});

$('[data-toggle="modal"]').click(function(){
	$('body').css('top','-100vh');
});
$('[data-dismiss="modal"]').click(function(){
	$('body').css('top','0');
});