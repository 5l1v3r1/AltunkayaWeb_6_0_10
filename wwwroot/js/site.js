/// <reference path="..\..\wwwroot\lib\jquery\dist\jquery.js" />

var tl = '<span class="tl">₺</span>';
var getUrl = window.location;
var baseUrl = getUrl.protocol + "//" + getUrl.host;

function startTime() {
	const today = new Date();
	let h = today.getHours();
	let m = today.getMinutes();
	let s = today.getSeconds();
	h = checkTime(h);
	m = checkTime(m);
	s = checkTime(s);
	document.getElementById('txt').innerHTML = h + ":" + m + ":" + s;
	setTimeout(startTime, 1000);
}

function checkTime(i) {
	if (i < 10) { i = "0" + i };
	return i;
}

function binlikAyiraci(value) {
	return Number(value).toLocaleString('tr');
}

function getCrypto(symbol) {

	$.ajax({
		url: 'https://api.binance.com/api/v3/ticker/bookTicker?symbol=' + symbol,
		type: 'GET',
		success: function (response) {
			setTimeout(getCrypto(symbol), 5000);
			document.getElementById(symbol).innerHTML = binlikAyiraci(response.askPrice);
		},
		error: function (jqXHR, textStatus) {
			//document.querySelectorAll("tr td").forEach(function (e) {
			//	e.firstChild.innerText = "CRYPTO";
			//});
			document.getElementById(symbol).textContent = "CRYPTO";
			setTimeout(getCrypto(symbol), 5000);
			//window.location.href = baseUrl + "/Home/CloseDrive/"; // Geri alınacak odunu keser nacak
			console.log("CRYPTO Request failed: " + this.status + "  > " + JSON.stringify(jqXHR));
		}
	});
}

function getDovizApi() {

	$.ajax({
		url: 'https://www.carsidoviz.com/doviz2.json?v=1670644189025',
		type: 'GET',
		success: function (response) {
			setTimeout(getDovizApi(), 5000);
			document.getElementById("DolarAlis").innerHTML = binlikAyiraci(response[0].dolaralis);
			document.getElementById("EuroAlis").innerHTML = binlikAyiraci(response[0].euroalis);
			document.getElementById("DolarSatis").innerHTML = binlikAyiraci(response[0].dolarsatis);
			document.getElementById("EuroSatis").innerHTML = binlikAyiraci(response[0].eurosatis);
		},
		error: function (jqXHR, textStatus) {
			document.getElementById("DolarAlis").textContent = "Doviz";
			document.getElementById("EuroAlis").textContent = "Doviz";
			document.getElementById("DolarSatis").textContent = "Doviz";
			document.getElementById("EuroSatis").textContent = "Doviz";
			setTimeout(getDovizApi(), 5000);
			console.log("Doviz Request failed: " + this.status + "  > " + JSON.stringify(jqXHR));
		}
	});
}

function getAltin() {

	$.ajax({
		url: '/Home/GetAltin',
		type: 'GET',
		success: function (response) {
			setTimeout(getAltin(), 5000)
			document.getElementById("Y22_Ayar_Bilezik_Alis").innerText = response.y22_Ayar_Bilezik_Alis;
			document.getElementById("Y22_Ayar_Bilezik_Satis").innerText = response.y22_Ayar_Bilezik_Satis;
			document.getElementById("Cumhuriyet_Ceyrak_Eski_Alis").innerText = response.cumhuriyet_Ceyrak_Eski_Alis;
			document.getElementById("Cumhuriyet_Ceyrak_Eski_Satis").innerText = response.cumhuriyet_Ceyrak_Eski_Satis;
			document.getElementById("Cumhuriyet_Yarim_Eski_Alis").innerText = response.cumhuriyet_Yarim_Eski_Alis;
			document.getElementById("Cumhuriyet_Yarim_Eski_Satis").innerText = response.cumhuriyet_Yarim_Eski_Satis;
			document.getElementById("Liralik_Eski_Alis").innerText = response.liralik_Eski_Alis;
			document.getElementById("Liralik_Eski_Satis").innerText = response.liralik_Eski_Satis;
		},
		error: function (jqXHR, textStatus) {

			document.getElementById("Y22_Ayar_Bilezik_Alis").innerText = "ALTIN";
			document.getElementById("Y22_Ayar_Bilezik_Satis").innerText = "ALTIN";
			document.getElementById("Cumhuriyet_Ceyrak_Eski_Alis").innerText = "ALTIN";
			document.getElementById("Cumhuriyet_Ceyrak_Eski_Satis").innerText = "ALTIN";
			document.getElementById("Cumhuriyet_Yarim_Eski_Alis").innerText = "ALTIN";
			document.getElementById("Cumhuriyet_Yarim_Eski_Satis").innerText = "ALTIN";
			document.getElementById("Liralik_Eski_Alis").innerText = "ALTIN";
			document.getElementById("Liralik_Eski_Satis").innerText = "ALTIN";
			setTimeout(getAltin(), 5000)
			//window.location.href = baseUrl + "/Home/CloseDrive/"; // Geri alınacak odunu keser nacak
			console.log("ALTIN Request failed: " + this.status + "  > " + JSON.stringify(jqXHR));
		}
	});
}

$(function () {

});
startTime();
getCrypto('BTCTRY');
getCrypto('ETHTRY');
getDovizApi();
getAltin();
