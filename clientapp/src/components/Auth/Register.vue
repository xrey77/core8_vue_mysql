<template>
<div class="modal fade" id="staticRegister" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticRegisterLabel" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">
      <div class="modal-header bg-danger">
        <h1 class="modal-title fs-5 text-white" id="staticRegisterLabel"><font-awesome-icon icon="elevator"/>&nbsp;Account Registration</h1>
        <button @click="closeRegistration" type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <form id="registrationForm" @submit.prevent="submitRegistration" autocomplete="off">
          <div class="row">
            <div class="col">
              <div class="mb-3">
                <input class="form-control" placeholder="enter Last Name" autocomplete="off" v-model="lastname" required type="text" />
              </div>
            </div>
            <div class="col">
              <div class="mb-3">
                <input type="text" required class="form-control" v-model="firstname" placeholder="enter First Name" autocomplete="off" />
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col">
              <div class="mb-3">
                <input type="email" required class="form-control" v-model="email" placeholder="enter Email Address" autocomplete="off">
              </div>
            </div>
            <div class="col">
              <div class="mb-3">
                <input type="text" required class="form-control" v-model="mobile" placeholder="enter Mobile No." autocomplete="off">
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col">
              <div class="mb-3">
                <input type="text" required class="form-control" v-model="username" placeholder="enter User Name" autocomplete="off">
              </div>
            </div>
            <div class="col">
              <div class="mb-3">
                <input type="password" required class="form-control" v-model="password" placeholder="enter Password" autocomplete="off">
              </div>
            </div>
          </div>

          <button type="submit" class="btn btn-danger mx-1">register</button>
          <button type="reset" class="btn btn-danger">reset</button>

        </form>

      </div>
      <div class="modal-footer">
          <div class="w-100 text-left text-danger">{{ registerMsg }}</div>
      </div>
    </div>
  </div>
</div>
</template>

<style scoped>
   #registerReset {
    visibility: hidden;
   }
</style>

<script lang="ts">
    import { defineComponent } from 'vue';
    import $ from 'jquery';
    import axios from 'axios';

    const api = axios.create({
      baseURL: "https://localhost:7241",
      headers: {'Accept': 'application/json',
                'Content-Type': 'application/json'}
    })

    export default defineComponent({
        name: 'Register-User',
        data() {
          return {
            lastname: '',
            firstname: '',
            email: '',
            mobile: '',
            username: '',
            password: '',
            registerMsg: ''
          }
        },
        methods:{
          closeRegistration: function() {
            this.lastname = '',
            this.firstname = '',
            this.email = '',
            this.mobile = '',
            this.username = '',
            this.password = '',
            this.registerMsg = '';
            $("#registerReset").click();
          },
          submitRegistration: function() {
            const data =JSON.stringify({ 
              lastname: this.lastname, firstname: this.firstname,
              email: this.email, mobile: this.mobile,
              username: this.username, password: this.password });
            api.post("/signup", data)
            .then((res) => {
                if (res.data.statuscode == 200) {
                    this.registerMsg = res.data.message;
                    return;
                } else {
                  this.registerMsg = res.data.message;
                  window.setTimeout(() => {
                    this.registerMsg = '';
                  }, 3000);
                  return;
                }
              }, (error) => {
                    this.registerMsg = error.message;
                    window.setTimeout(() => {
                    this.registerMsg = '';
                  }, 3000);
                  return;
            });

            $("#registerReset").click();

          }
        }
    })
</script>