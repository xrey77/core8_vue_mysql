<template>
<div class="modal fade" id="staticMfa" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticMfaLabel" aria-hidden="true">
  <div class="modal-dialog modal-sm modal-dialog-centered">
    <div class="modal-content">
      <div class="modal-header bg-warning">
        <h1 class="modal-title fs-5 text-white" id="staticMfaLabel">2-Factor Authenticator</h1>
        <button @click="closeMfa" type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <form id="mfaForm" @submit.prevent="submitOTP">
          <div class="mb-3">
            <input type="text" class="form-control" v-model="Otpcode" placeholder="enter 6 digits OTP Code" autocomplete="off">
        </div>
        <button type="submit" class="btn btn-warning">submit</button>
        <button id="mfaReset" type="reset" class="btn btn-warning">reset</button>

        </form>
      </div>
      <div class="modal-footer">
        <div id="mfaMsg" class="w-100 text-left text-danger">{{ OtpMessage }}</div>
      </div>
    </div>
  </div>
</div>  
</template>

<style scoped>
  #mfaReset {
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
    name: 'Mfa-Page',
    data() {
        return {
            Userid: '',
            Otpcode: '',
            OtpMessage: ''
        }
    },
    mounted() {
        const usrId = sessionStorage.getItem("USERID")
        if (usrId) {
          this.Userid = usrId
        }
    },
    methods: {
        submitOTP: function() {
            const data =JSON.stringify({ id: this.Userid, otp: this.Otpcode });
            api.post("/api/validateotp", data)
            .then((res) => {
                if (res.data.statuscode == 200) {
                    this.OtpMessage = res.data.message;
                    sessionStorage.setItem("USERNAME", res.data.username);
                    $("#mfaReset").click();
                    window.setTimeout(() => {
                      this.OtpMessage = '';
                      window.location.reload();
                    }, 3000);
                    return;
                } else {
                  this.OtpMessage = res.data.message;
                  window.setTimeout(() => {
                    this.OtpMessage = '';
                    }, 3000);
                    return;
                }
              }, (error) => {
                    this.OtpMessage = error.message;
                    window.setTimeout(() => {
                    this.OtpMessage = '';
                    }, 3000);
                    return;
            });

        },
        closeMfa: function() {
            $("#mfaReset").click();
            this.OtpMessage = '';
            sessionStorage.removeItem('USERID');
            sessionStorage.removeItem('USERNAME');
            sessionStorage.removeItem('TOKEN');
            sessionStorage.removeItem('USERPIC');            
            sessionStorage.clear();
            window.location.href="/";
        },
    }
 })
</script>