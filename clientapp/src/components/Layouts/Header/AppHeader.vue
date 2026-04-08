<template>
    <nav class="navbar navbar-expand-lg bg-body-tertiary">
        <div v-cloak class="container-fluid">
            <router-link class="navbar-brand" to="/"><img class="logo" src="/logo.png" alt="" /></router-link>   
          <button class="navbar-toggler" type="button"  data-bs-toggle="offcanvas" data-bs-target="#offcanvasMenu" aria-controls="offcanvasWithBothOptions">
            <span class="navbar-toggler-icon"></span>
          </button>
          <div class="collapse navbar-collapse" id="navbarSupportedContent">
            <ul class="navbar-nav me-auto mb-2 mb-lg-0">
              <li class="nav-item">
                <router-link class="nav-link active" to="/about"><font-awesome-icon icon="circle-question"/>About Us</router-link>                
              </li>
              <li class="nav-item dropdown">
                <a class="nav-link dropdown-toggle active" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                  <font-awesome-icon icon="chart-area"/>Products
                </a>
                <ul class="dropdown-menu">
                  <li>
                    <router-link class="dropdown-item" to="/productlist">Product List</router-link>                
                </li>
                  <li>
                    <router-link class="dropdown-item" to="/productcatalogs">Catalogs</router-link>                
                </li>
                  <li><hr class="dropdown-divider"></li>
                  <li>
                    <router-link class="dropdown-item" to="/productsearch">Search Product</router-link>                
                </li>
                </ul>
              </li>
              <li class="nav-item">
                <router-link class="nav-link active" to="/location"><font-awesome-icon icon="location-arrow"/>Location</router-link>                
              </li>
            </ul>
            <div v-if="username === ''">
              <ul class="navbar-nav mr-auto">
                <li class="nav-item">
                  <a class="nav-link active" href="/#" data-bs-toggle="modal" data-bs-target="#staticLogin"><font-awesome-icon icon="unlock"/>Login</a>                
                </li>
                <li class="nav-item">
                  <a class="nav-link active" href="/#" data-bs-toggle="modal" data-bs-target="#staticRegister"><font-awesome-icon icon="elevator"/>Register</a>                
                </li>            
              </ul>
            </div>
            <ul v-if="username !== ''" class="navbar-nav mr-auto">
                  <li class="nav-item dropdown">
                    <a class="nav-link dropdown-toggle active" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                      <img class="user" v-bind:src="userpic" />&nbsp;{{ username }}
                    </a>
                    <ul class="dropdown-menu">
                      <li>
                        <a @click="logout" class="dropdown-item" href="/#">LogOut</a>
                    </li>
                      <li>
                        <router-link class="dropdown-item" to="/profile">Profile</router-link>                
                    </li>
                      <li><hr class="dropdown-divider"></li>
                      <li>
                        <router-link class="dropdown-item" to="/#">Messenger</router-link>                
                    </li>
                    </ul>
                  </li>              
                </ul>


          </div>

        </div>
    </nav>
    <!-- OFFCANVAS MENU -->
    <div class="offcanvas offcanvas-end" data-bs-scroll="true" tabindex="-1" id="offcanvasMenu" aria-labelledby="offcanvasWithBothOptionsLabel">
      <div class="offcanvas-header bg-primary">
        <h5 class="offcanvas-title text-white" id="offcanvasWithBothOptionsLabel">Drawer Menu</h5>
        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="offcanvas" aria-label="Close"></button>
      </div>
      <div class="offcanvas-body">

        <ul class="nav flex-column">
          <li class="nav-item" data-bs-dismiss="offcanvas">
            <router-link class="nav-link active" to="/about">About Us</router-link>                
          </li>
          <li class="nav-item"><hr/></li>
          <li class="nav-item dropdown">
                <a class="nav-link dropdown-toggle active" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                  Products
                </a>
                <ul class="dropdown-menu">
                  <li data-bs-dismiss="offcanvas">
                    <router-link class="dropdown-item" to="/productlist">Product List</router-link>                
                </li>
                  <li data-bs-dismiss="offcanvas">
                    <router-link class="dropdown-item" to="/productcatalogs">Catalogs</router-link>                
                </li>
                  <li><hr class="dropdown-divider"></li>
                  <li data-bs-dismiss="offcanvas">
                    <router-link class="dropdown-item" to="/productsearch">Search Product</router-link>                
                </li>
                </ul>
              </li>
              <li class="nav-item"><hr/></li>

              <li class="nav-item" data-bs-dismiss="offcanvas">
                <router-link class="nav-link active" to="/location">Locate Us</router-link>                
              </li>
              <li class="nav-item"><hr/></li>

            </ul>
            <div v-if="username === ''">
              <ul class="nav flex-column">
                <li class="nav-item" data-bs-dismiss="offcanvas">
                  <a class="nav-link active" href="/#" data-bs-toggle="modal" data-bs-target="#staticLogin">Login</a>
                </li>
                <li class="nav-item"><hr/></li>

                <li class="nav-item" data-bs-dismiss="offcanvas">
                  <a class="nav-link active" href="/#" data-bs-toggle="modal" data-bs-target="#staticRegister">Register</a>
                </li>            
              </ul>
            </div>
            <ul v-if="username !== ''"  class="navbar-nav mr-auto">
                  <li class="nav-item dropdown">
                    <a class="nav-link dropdown-toggle active" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                      <img class="user"  v-bind:src="userpic" />{{ username }}
                    </a>
                    <ul class="dropdown-menu">
                      <li data-bs-dismiss="offcanvas">
                        <a @click="logout" class="dropdown-item" href="/#">LogOut</a>
                      </li>
                      <li class="nav-item"><hr/></li>
                      <li data-bs-dismiss="offcanvas">
                        <router-link class="dropdown-item" to="/profile">Profile</router-link>                
                      </li>
                      <li><hr class="dropdown-divider"></li>
                      <li data-bs-dismiss="offcanvas">
                        <router-link class="dropdown-item" to="/#">Messenger</router-link>                
                      </li>
                    </ul>
                  </li>              
        </ul>        

      </div>
    </div>
    

    <router-view />
    <Register/>
    <Login/>
</template>

<style lang="scss" scoped>
.slide-img-enter-from {
  left: -100%;
  transform: translate(0, 0);
}
/* this one is actually not needed at all */
.slide-img-leave-from {
  transform: translate(0, 0);
}
  .logo {
    width: 120px;
    height: 25px;
  }
  .user {
    width: 50px;
    height: auto;
    border-radius: 50%;
  }

#offcanvasMenu {
  max-width: 70%!important;
}

</style>

<script setup lang="ts">
import {ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import Register from '../../Auth/Register.vue';
import Login from '../../Auth/Login.vue';

/* eslint-disable */
defineOptions({
    name: 'AppHeader',
    inheritAttrs: false
});

const router = useRouter();

const username = ref('');
const userpic = ref('');

onMounted(() => {
    const usrname = sessionStorage.getItem('USERNAME');
    if (usrname) username.value = usrname;

    const usrpic = sessionStorage.getItem('USERPIC');
    if (usrpic) userpic.value = usrpic;
});

const logout = async () => {
    sessionStorage.removeItem('USERID');
    sessionStorage.removeItem('USERNAME');
    sessionStorage.removeItem('TOKEN');
    sessionStorage.removeItem('USERPIC');
    sessionStorage.clear();
    await router.push('/');
    window.location.reload();    
};

</script>
