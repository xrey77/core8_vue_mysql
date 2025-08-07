<template>
<div class="container-fluid mb-4">
    <h2>Search Product</h2>    
    <form class="row g-3" @submit.prevent="getProdsearch(page)" autocomplete="off">
        <div class="col-auto">
          <input type="text" required class="form-control-sm" v-model="search" name="search" placeholder="enter Product keyword">
        </div>
        <div class="col-auto">
          <button type="submit" class="btn btn-primary btn-sm mb-3">search</button>
        </div>
    </form>


    <div v-if="totpage !== 0" class="container-fluid mb-4">
        <div class="card-group">
            <div v-for="prod in prodsearch" :key="prod.id" class="card">
                <img v-bind:src="prod.productPicture" class="card-img-top product-size" alt=""/>
                <div class="card-body">
                    <h5 class="card-title">Description</h5>
                    <p class="card-text">{{prod.descriptions}}</p>
                </div>
                <div class="card-footer price-size">
                    <p class="card-text text-danger"><span class="text-dark">PRICE :</span>&nbsp;<strong>&#8369;{{prod.sellPrice.toFixed(2)}}</strong></p>
                </div>  
            </div>
        
        </div>    
        <nav aria-label="Page navigation example">
            <ul class="pagination mt-2 mb-4">
                <li class="page-item"><a @click="firstPage($event)" class="page-link" href="#">First</a></li>
                <li class="page-item"><a @click="prevPage($event)" class="page-link" href="#">Previous</a></li>
                <li class="page-item"><a @click="nextPage($event)" class="page-link" href="#">Next</a></li>
                <li class="page-item"><a @click="lastPage($event)" class="page-link" href="#">Last</a></li>              
                <li class="page-item page-link text-danger">Page&nbsp;{{page}} of&nbsp;{{totpage}}</li>
            </ul>
          </nav>
    </div>

</div>
</template>

<style scoped>
.card-group {
    width: 100%!important;
}
.card-size {
    width: 300px!important;
}
.product-size {
    width: 220px!important;
    height: 300px!important;
}
.price-size {
    width: 215px;
}
@media (max-width: 991.98px) {
    .pagination {
        font-size: 12px;
    }
    nav {
        margin-bottom: 50px;
    }
    .price-size {
        width: 100%;
    }
    .card-group {
        width: -50px;
    }    
 }
</style>

<script lang="ts">
    import { defineComponent } from 'vue';
    import axios from 'axios';

    const api = axios.create({
        baseURL: "https://localhost:7241",
        headers: {'Accept': 'application/json',
                'Content-Type': 'application/json'}
    })    

    export default defineComponent({
        name: 'Search-Page',
        data() {
            return {
                search: '',
                prodsearch: [],
                page: 1,
                totpage: 0,
                totRecs: 0,
                errors: [],            
            }
        },
        methods: {
            /* eslint-disable */
            getProdsearch: function(page: number) {
                api.get(`/api/searchproducts/${page}/${this.search}`)
                .then((res) => {
                    console.log(res.data.products);
                    this.prodsearch = res.data.products;
                    this.totpage = res.data.totpage;
                    this.page = res.data.page;
                }, (error) => {
                        console.log(error.message);
                        return;
                });
            },
            nextPage: function(event: any) {
            event.preventDefault();    
            if (this.page == this.totpage) {
                return;
            }
            this.page = this.page + 1;
            this.getProdsearch(this.page);
            return;
        },
        prevPage: function(event: any) {
            event.preventDefault();    
            if (this.page == 1) {
            return;
            }
            this.page = this.page - 1;
            this.getProdsearch(this.page);
            return;    
        },
        firstPage: function(event: any) {
            event.preventDefault();    
            this.page = 1;
            this.getProdsearch(this.page);
            return;    
        },
        lastPage: function(event: any) {
            event.preventDefault();    
            this.page = this.totpage;
            this.getProdsearch(this.page);
            return;    

        }            
        },   
})
</script>