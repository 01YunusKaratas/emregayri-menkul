
document.addEventListener("DOMContentLoaded", function () {
    var swiper = new Swiper(".autoslider3reverse", {
        direction: "vertical", // Dikey kaydırma
        loop: true, // Sonsuz döngü
        slidesPerView: 1, // Aynı anda 1 slide göster
        spaceBetween: 10, // Slide'lar arasındaki boşluk
        speed: 10000, // 3 saniyede bir kayma
        autoplay: {
            delay: 0, // Kesintisiz çalışma
            disableOnInteraction: false, // Kullanıcı müdahale etse de devam etsin
        },
        observer: true, // DOM değişikliklerini izle
        observeParents: true, // Üst element değişirse güncelle
    });
});
