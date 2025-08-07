<template>
  <div>
  <div id="carouselExampleCaptions" v-cloak class="carousel slide">
    <div class="carousel-indicators">
      <button type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
      <button type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide-to="1" aria-label="Slide 2"></button>
      <button type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide-to="2" aria-label="Slide 3"></button>
      <button type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide-to="3" aria-label="Slide 4"></button>
      <button type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide-to="4" aria-label="Slide 5"></button>      
    </div>
    <div class="carousel-inner">
      <div class="carousel-item active" data-bs-interval="10000">
        <img src="/images/1.png" class="d-block w-100" alt="...">
        <div class="carousel-caption d-none d-md-block">
          <h1 class="embossed-text">Touch Screen Self-Service Terminal</h1>
          <h5 class="embossed-text">Fully enclosed monitors. NEMA 12/4/4X (IP65/IP66) or NEMA 2 (IP22) rated. Touch Screen options available.</h5>
        </div>
      </div>
      <div class="carousel-item">
        <img src="/images/2.png" class="d-block w-100" alt="...">
        <div class="carousel-caption d-none d-md-block">
          <h1 class="embossed-text">Interactive Digital Maps.</h1>
          <h5 class="embossed-text">
            A "self service location map" is an interactive kiosks that provide maps and directions
          </h5>
        </div>
      </div>
      <div class="carousel-item">
        <img src="/images/3.png" class="d-block w-100" alt="...">
        <div class="carousel-caption d-none d-md-block">
          <h1 class="embossed-text">Self-Service Express Terminal</h1>
          <h5 class="embossed-text">
            Implementing a self-service solution can streamline your business operations and reduce costs while increasing customer satisfaction and return visits.
          </h5>
        </div>
      </div>
      <div class="carousel-item">
        <img src="/images/4.png" class="d-block w-100" alt="...">
        <div class="carousel-caption d-none d-md-block">
          <h1 class="embossed-text">Retail ATM Solutions</h1>
          <h5 class="embossed-text">
            An ATM is more than just a cash machine—it's a powerful tool to expand financial access, driving foot traffic, enhancing customer service and boosting sales.             
          </h5>
        </div>
      </div>
      <div class="carousel-item">
        <img src="/images/5.png" class="d-block w-100" alt="...">
        <div class="carousel-caption d-none d-md-block">
          <h1 class="embossed-text">Touch Digital Interactive Kiosk</h1>
          <h5 class="embossed-text">
            Touch digital interactive kiosks are becoming more and more popular because of their practicality and versatility.
          </h5>
        </div>
      </div>

    </div>
    <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide="prev">
      <span class="carousel-control-prev-icon" aria-hidden="true"></span>
      <span class="visually-hidden">Previous</span>
    </button>
    <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide="next">
      <span class="carousel-control-next-icon" aria-hidden="true"></span>
      <span class="visually-hidden">Next</span>
    </button>
  </div>
  <div class="card card-2 mt-3 mb-5">
    <div class="card-header bg-dark"><h4 class="text-light text-center">Transforming the Way People Bank and Shop.</h4></div>
    <div class="card-body text-success">
      <h5 class="card-title">Plan Ahead This Peak Season with DN AllConnect Services</h5>
      <p class="card-text">
        Now is the time to lay the groundwork for a successful peak season. With the right services in place, retailers can avoid disruption, reduce complexity, and stay focused on revenue-driving tasks.
      </p>
    </div>
    <div class="card-footer bg-transparent border-success"></div>
  </div>  
</div>
</template>

<style lang="scss" scoped>
.embossed-text {
  color: #eaf0a0; /* Text color should match or be very close to the background */
  text-shadow:
      -1px -1px 0px rgba(255, 255, 255, 0.7), /* Light highlight (top-left) */
      1px 1px 0px rgba(0, 0, 0, 0.5);      /* Dark shadow (bottom-right) */
}
.card-2 {
  width: 99%!important;
  margin: 0 auto;
}
.slide-img-enter-from {
  left: -100%;
  transform: translate(0, 0);
}
/* this one is actually not needed at all */
.slide-img-leave-from {
  transform: translate(0, 0);
}
.carousel-indicators [data-bs-target] {
  box-sizing: content-box;
  flex: 0 1 auto;
  width: 10px;
  height: 10px;
  padding: 0;
  margin-right: 3px;
  margin-left: 3px;
  text-indent: -999px;
  cursor: pointer;
  background-color: #fff;
  background-clip: padding-box;
  border: 0;
  border-top: 10px solid transparent;
  border-bottom: 10px solid transparent;
  opacity: .5;
  transition: opacity .6s ease;
}

.carousel-inner .carousel-item img {
  max-width: 100% !important;
  height: 550px;
  object-fit: cover;
}
</style>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'Home-Page',
  components: {    
  },
});
</script>
