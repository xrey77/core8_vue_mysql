<template>

<div class="card card-width bs-info-border-subtle">
  <div class="card-header bg-info text-light">
    <strong>USER'S PROFILE NO.&nbsp; {{ userid }}</strong>
  </div>
  <div class="card-body">

    <form id="profileForm" @submit.prevent="submitProfile" enctype="multipart/form-data" method="POST">
        <div class="row">
            <div class="col">
                <div class="mb-3">
                    <input type="text" required v-model="firstname" class="form-control"  autocomplete="off">
                </div>
                <div class="mb-3">
                    <input type="text" required v-model="lastname" class="form-control" autocomplete="off">
                </div>
                <div class="mb-3">
                    <input type="email" v-model="email" class="form-control" readonly>
                </div>
                <div class="mb-3">
                    <input type="text" required v-model="mobile" class="form-control" autocomplete="off">
                </div>

            </div>
            <div class="col">
                <img id="userpic" class="usr" v-bind:src="profilepic" alt=""/>
                <div class="mb-3">
                    <input type="file" @change="changePicture($event)" class="form-control form-control-sm mt-3" accept=".png, .jpg, .jpeg, .gif"  />
                </div>

            </div>
        </div>

        <div class="row">
            <div class="col">
                <div class="form-check">
                    <input type="checkbox" class="form-check-input" v-model="chgPwd" @change="checkboxPassword" />
                    <label class="form-check-label" for="chgPwd">
                        Change Password
                    </label>
                </div>
            </div>
            <div class="col">
                <div class="form-check">
                    <input class="form-check-input" type="checkbox" v-model="chkMfa" @change="checkboxMFA" />
                    <label class="form-check-label" for="chkMfa">
                        2-Factor Authenticator
                    </label>
                </div>
            </div>
        </div>

        <div class="row">

            <div class="col">
              <div id="cpwd">
                <div class="mb-3">
                    <input type="password"  v-model="password" class="form-control pwd" placeholder="enter new Password" autocomplete="off">
                </div>
                <div class="mb-3">
                    <input type="password" v-model="confpassword" class="form-control pwd" placeholder="confirm new Password" autocomplete="off">
                </div>
                <button @click="changePassword" type="button" class="btn btn-primary">change password</button>
              </div>
                <div id="mfa1">
                        <div v-if="qrcodeurl != ''">
                            <img class="qrcode1" v-bind:src="qrcodeurl" alt="qrcodeurl"/>
                        </div>
                        <div v-if="qrcodeurl === ''">
                            <img class="qrcode2" src="/qrcode.png" alt="QRCODE" />
                        </div>

                </div>

            </div>
            <div class="col">
                <div id="mfa2">
                        <p id="qrcode-cap1" class='text-danger'><strong>Requirements</strong></p>
                        <p id="qrcode-cap2">You need to install <strong>Google or Microsoft Authenticator</strong> in your Mobile Phone, once installed, click Enable Button below, and <strong>SCAN QR CODE</strong>, next time you login, another dialog window will appear, then enter the <strong>OTP CODE</strong> from your Mobile Phone in order for you to login.</p>
                        <div class="row">
                            <div class="col btn-1">
                                <button @click="enableMFA" type="button" class="btn btn-primary qrcode-cap3">Enable</button>
                            </div>
                            <div class="col col-3 btn-2">
                                <button @click="disableMFA" type="button" class="btn btn-secondary qrcode-cap3">Disable</button>
                            </div>
                        </div>

                    </div>

            </div>
        </div>
        <div v-if="showSave === false">
            <button id="save" type="submit" class="btn btn-info">save</button>
        </div>
    </form>
  </div>
  <div class="card-footer">
    <div class="w-100 text-left text-danger">{{ profileMsg }}</div>
  </div>
</div>
</template>

<style scoped>
    .usr {
        width: 150px!important;
        height: 150px!important;
    }
    .card-width {
        padding: 20px!important;
    }
    .btn-1 {
        max-width: 90px!important;
    }
    .btn-2 {
        float: left!important;
    }
    #save {
        margin-top: 30px!important;
    }
    .qrcode1 {
        float: right;
        width: 200px;
        height: 200px;
    }
    .qrcode2 {
        float: right;
        width: 200px;
        height: 200px;
        opacity: 0.3;
    }
    @media (max-width: 991.98px) { 
        #qrcode-cap1 {
            margin-top: 200px;
            margin-left: -210px;
        }

        #qrcode-cap2 {
            margin-top: 10px;
            margin-left: -230px;
            width: 300px;
            text-align: justify;
        }

        .qrcode-cap3  {
            margin-left: -230px !important;
        }
    }
</style>

<script lang="ts">
import { defineComponent, ref } from 'vue'
import $ from 'jquery';
import axios from 'axios';

const selectedFile = ref<File | null>(null);

const api = axios.create({
    baseURL: "https://localhost:7241",
    headers: {'Accept': 'application/json',
              'Content-Type': 'application/json'}
})

export default defineComponent({
    name: 'Profile-Page',
    data() {
        return {
            userid: '',
            token: '',
            lastname: '',
            firstname: '',
            email: '',
            mobile: '',
            password: '',
            confpassword: '',
            qrcodeurl: '',
            profileMsg: '',
            profilepic: "",
            chgPwd: false,
            chkMfa: false,
            showSave: false,
        }
    },
    mounted(){
        const _usrid = sessionStorage.getItem('USERID');
        if (_usrid) {
            this.userid = _usrid
        }
        const _token = sessionStorage.getItem("TOKEN");
        if (_token) {
            this.token = _token
        }
        $("#cpwd").hide();
        $("#mfa1").hide();
        $("#mfa2").hide();  

        api.get(`/api/getbyid/${this.userid}`, { headers: {
            Authorization: `Bearer ${this.token}`
        }} )
            .then((res) => {
                if (res.data.statuscode == 200) {
                    this.profileMsg = res.data.user.message;
                    this.firstname = res.data.user.firstname;
                    this.lastname = res.data.user.lastname;
                    this.email = res.data.user.email;
                    this.mobile = res.data.user.mobile;
                    this.profilepic = res.data.user.profilepic;
                    this.qrcodeurl = res.data.user.qrcodeurl;
                    return;
                } else {
                  this.profileMsg = res.data.message;
                    return;
                }
              }, (error) => {
                    this.profileMsg = error.message;
                    return;
            });


    },
    methods: {
        submitProfile: function() {
            const data =JSON.stringify({ lastname: this.lastname, 
                firstname: this.firstname, mobile: this.mobile });

            api.patch(`/api/updateprofile/${this.userid}`, data, { headers: {
            Authorization: `Bearer ${this.token}`
            }} )
            .then((res) => {
                if (res.data.statuscode == 200) {
                    this.profileMsg = res.data.message;
                    window.setTimeout(() => {
                        this.profileMsg = '';
                    }, 3000);
                    return;
                } else {
                  this.profileMsg = res.data.message;
                  window.setTimeout(() => {
                        this.profileMsg = '';
                    }, 3000);
                    return;
                }
              }, (error) => {
                    this.profileMsg = error.message;
                    window.setTimeout(() => {
                        this.profileMsg = '';
                    }, 3000);
                    return;
            });

        },
        changePassword: function() {
            if (this.password === '') {
                this.profileMsg = "Please enter New Password.";
                setTimeout(() => {
                    this.profileMsg = '';
                }, 3000);
                return;
            }
            if (this.confpassword === '') {
                this.profileMsg = "Please confirm New Password.";
                setTimeout(() => {
                    this.confpassword = '';
                }, 3000);
                return;
            }
            if (this.password != this.confpassword) {
                this.profileMsg = "New Password does not matched.";
                setTimeout(() => {
                    this.profileMsg = '';
                }, 3000);
                return;
            }
            const data =JSON.stringify({ password: this.password });
            api.patch(`/api/updatepassword/${this.userid}`, data, { headers: {
            Authorization: `Bearer ${this.token}`
            }} )
            .then((res) => {
                if (res.data.statuscode == 200) {
                    this.profileMsg = res.data.message;
                    window.setTimeout(() => {
                        this.profileMsg = '';
                    }, 3000);
                    return;
                } else {
                  this.profileMsg = res.data.message;
                  window.setTimeout(() => {
                        this.profileMsg = '';
                    }, 3000);
                    return;
                }
              }, (error) => {
                    this.profileMsg = error.message;
                    window.setTimeout(() => {
                        this.profileMsg = '';
                    }, 3000);
                    return;
            });


        },
        changePicture: function(event: Event) {
            event.preventDefault();            
            const target = event.target as HTMLInputElement;
            if (target.files && target.files.length > 0) {
                selectedFile.value = target.files[0];
                $("#userpic").attr('src',URL.createObjectURL(selectedFile.value));
            }

            if (selectedFile.value) {
                let formdata = new FormData();
                formdata.append('Id', this.userid);
                formdata.append('Profilepic', selectedFile.value);

                api.post("/api/uploadpicture", formdata, { headers: {
                'Content-Type': 'multipart/form-data',
                Authorization: `Bearer ${this.token}`
                }} )
                .then((res) => {
                    if (res.data.statuscode == 200) {
                        this.profileMsg = res.data.message;
                        window.setTimeout(() => {
                            sessionStorage.setItem('USERPIC',res.data.profilepic);
                            this.profileMsg = '';
                            window.location.reload();
                        }, 3000);
                        return;
                    } else {
                    this.profileMsg = res.data.message;
                    window.setTimeout(() => {
                            this.profileMsg = '';
                        }, 3000);
                        return;
                    }
                }, (error) => {
                        this.profileMsg = error.message;
                        window.setTimeout(() => {
                            this.profileMsg = '';
                        }, 3000);
                        return;
                });
          } //end-selectedFile


        },
        checkboxPassword: function() {
            if (this.chgPwd) {
                $("#cpwd").show();
                $("#mfa1").hide();  
                $("#mfa2").hide();  
                this.chkMfa = false;
                this.showSave = true;
                // $('#chkMfa').prop('checked', false);
            } else {
                this.password = '';
                this.confpassword = '';
                this.showSave = false;
                $("#cpwd").hide();
            }
        },
        checkboxMFA: function() {
            if (this.chkMfa) {
                $("#cpwd").hide();
                $("#mfa1").show();
                $("#mfa2").show();
                this.chgPwd = false;
                this.showSave = true;
                // $('#chkPwd').prop('checked', false);
            } else {
                $("#mfa1").hide();  
                $("#mfa2").hide();                  
                this.showSave = false;
            }
        },
        enableMFA: function() {
            const data =JSON.stringify({ Twofactorenabled: true });
            api.put("/api/enablemfa/" + this.userid, data, { headers: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${this.token}`
            }} )
            .then((res) => {
                if (res.data.statuscode == 200) {
                    this.qrcodeurl = res.data.qrcode;
                    this.profileMsg = res.data.message;
                    window.setTimeout(() => {
                        this.profileMsg = '';
                    }, 3000);
                    return;
                } else {
                  this.profileMsg = res.data.message;
                  window.setTimeout(() => {
                        this.profileMsg = '';
                    }, 3000);
                    return;
                }
              }, (error) => {
                    this.profileMsg = error.message;
                    window.setTimeout(() => {
                        this.profileMsg = '';
                    }, 3000);
                    return;
            });
        },
        disableMFA: function() {
            const data =JSON.stringify({ Twofactorenabled: false });
            api.put("/api/enablemfa/" + this.userid, data, { headers: {
                Authorization: `Bearer ${this.token}`
            }} )
            .then((res) => {
                if (res.data.statuscode == 200) {
                    this.qrcodeurl = '';
                    this.profileMsg = res.data.message;
                    window.setTimeout(() => {
                        this.profileMsg = '';
                    }, 3000);
                    return;
                } else {
                  this.profileMsg = res.data.message;
                  window.setTimeout(() => {
                        this.profileMsg = '';
                    }, 3000);
                    return;
                }
              }, (error) => {
                    this.profileMsg = error.message;
                    window.setTimeout(() => {
                        this.profileMsg = '';
                    }, 3000);
                    return;
            });

        },

    }

})
</script>
