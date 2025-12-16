
$(document).ready ( function(){
	
});

$.ajaxSetup ({
    // Disable caching of AJAX responses
    cache: false
});
		
/* Ajax post to increase the display quantity of a product with requested product id
 * in the cart, on the shop page */   
function addCart(id) {
	
	$.ajax({
	  type: 'POST',
      url: 'Cart/AddToCart',
      data: { id: id },
	  success: function(prodQTY){
		var inputID = "#inputQTY_" + id; 
		$(inputID).val(prodQTY).change();
      }
    });
	
}

/* Ajax post to decrease the display quantity of a product with requested product id
 * in the cart, on the shop page */
function removeCart(id) {
	
	$.ajax({
	  type: 'POST',
      url: 'Cart/RemoveFromCart',
      data: { id: id },
      success: function(prodQTY){
		var inputID = "#inputQTY_" + id; 
		$(inputID).val(prodQTY).change();
	  }
    });
	
}

$('[data-onload]').each(function(){
    eval($(this).data('onload'));
});

/* Ajax post to update the display quantity of a product with requested product id
 * in the cart, on the shop page, when the page is refreshed */
function updateQTY(qtyID) {
	
	$.ajax({
		type: 'POST',
		url: 'Shop/UpdateQTY',
		data: { qtyID: qtyID },
		success: function(qty){
			var inputID = "#inputQTY_" + qtyID; 
			$(inputID).val(qty).change();
		}
    });
	
}