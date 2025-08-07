<template>
<div>
<div class="modal fade" v-cloak id="staticLogin" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticLoginLabel" aria-hidden="true">
  <div class="modal-dialog modal-sm modal-dialog-centered">
    <div class="modal-content">
      <div class="modal-header bg-primary">
        <h1 class="modal-title fs-5 text-white" id="staticLoginLabel"><font-awesome-icon icon="unlock"/>&nbsp;User's Login</h1>
        <button id="closeLogin" @click="closeLogin" type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <form id="loginForm" @submit.prevent="submitLogin" autocomplete="off">
          <div class="mb-3">
            <input type="text" required class="form-control" v-model="Username" placeholder="enter Username" autocomplete="off">
        </div>
        <div class="mb-3">
            <input type="password" required class="form-control" v-model="Password" placeholder="enter Password" autocomplete="off">
        </div>
        <button type="submit" class="btn btn-primary mx-1">login</button>
        <button type="reset" class="btn btn-primary">reset</button>

        <button id="mfaModal" type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#staticMfa">mfa</button>

        </form>
      </div>
      <div class="modal-footer">
        <div id="loginMsg" class="w-100 text-left text-danger">{{ LoginMessage }}</div>
      </div>
    </div>
  </div>
</div>
<Mfa/>
</div>
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

  #loginReset {
    visibility: hidden;
  }
  #mfaModal {
    visibility: hidden;
  }

  #loginMsg {
    font-size: 12px;
  }
</style>

<script lang="ts">
    import { defineComponent } from 'vue';
    import Mfa from '../../views/Mfa.vue';
    import $ from 'jquery';
    import axios from 'axios';

    const api = axios.create({
        baseURL: "https://localhost:7241",
        headers: {'Accept': 'application/json',
                  'Content-Type': 'application/json'}
    })

  export default defineComponent({
    name: 'Login-Page',
    components: {
      Mfa,
    },
    data() {
      return {
        Username: '',
        Password: '',
        LoginMessage: ''
      }
    },
    methods: {
        closeLogin: function() {
            this.Username = '';
            this.Password = '';
            this.LoginMessage = '';
            $("#loginReset").click();
        },
        submitLogin: function() {    
            $('input').prop('disabled', true);
            this.LoginMessage = 'Please wait...';
            const data =JSON.stringify({ username: this.Username, password: this.Password });
            api.post("/signin", data)
            .then((res) => {
                if (res.data.statuscode == 200) {
                    this.LoginMessage = res.data.message;
                    if (res.data.qrcodeurl !== null) {
                        sessionStorage.setItem('USERID',res.data.id);
                        sessionStorage.setItem('TOKEN',res.data.token);
                        sessionStorage.setItem('ROLE',res.data.roles);
                        sessionStorage.setItem('USERPIC',res.data.profilepic);
                        this.Username = '';
                        this.Password = '';
                        this.LoginMessage = '';
                        $("#loginReset").click();
                        $("#mfaModal").click();

                    } else {
                        sessionStorage.setItem('USERID',res.data.id);
                        sessionStorage.setItem('USERNAME',res.data.username);
                        sessionStorage.setItem('TOKEN',res.data.token);                        
                        sessionStorage.setItem('ROLE',res.data.roles);
                        sessionStorage.setItem('USERPIC',res.data.profilepic);
                          this.Username = '';
                          this.Password = '';
                          this.LoginMessage = '';
                        $("#loginReset").click();
                        window.setTimeout(function() {
                          window.location.reload();
                        }, 200);
                    }
                    return;
                } else {
                  $('input').prop('disabled', false);
                  this.LoginMessage = res.data.message;
                  window.setTimeout(() => {
                        this.LoginMessage = '';
                        this.Password = '';
                    }, 3000);
                    return;
                }
              }, (error) => {
                    $('input').prop('disabled', false);
                    this.LoginMessage = error.message;
                    window.setTimeout(() => {
                        this.LoginMessage = '';
                    }, 3000);
                    return;
            });

        },        
      }
    })
</script>