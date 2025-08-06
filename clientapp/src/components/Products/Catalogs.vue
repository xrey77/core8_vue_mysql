<template>
<div class="container-fluid mb-4">
    <h4 class="text-center">Product Catalogs</h4>
    <div class="card-group">
        <div v-for="prod in catalogs" :key="prod.id" class="card">
            <img v-bind:src="prod.prod_pic" class="card-img-top" alt=""/>
            <div class="card-body">
                <h5 class="card-title">Descriptions</h5>
                <p class="card-text">{{prod.descriptions}}</p>
            </div>
            <div class="card-footer">
                <p class="card-text text-danger"><span class="text-dark">PRICE :</span>&nbsp;<strong>&#8369;{{prod.sell_price}}</strong></p>
            </div>  
        </div>
    
    </div>    
    <nav aria-label="Page navigation example">
        <ul class="pagination mt-4">
          <li class="page-item"><a @click="lastPage($event)" class="page-link" href="#">Last</a></li>
          <li class="page-item"><a @click="prevPage($event)" class="page-link" href="#">Previous</a></li>
          <li class="page-item"><a @click="nextPage($event)" class="page-link" href="#">Next</a></li>
          <li class="page-item"><a @click="firstPage($event)" class="page-link" href="#">First</a></li>
          <li class="page-item page-link text-danger">Page&nbsp;{{page}} of&nbsp;{{totpage}}</li>
        </ul>
      </nav>
</div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';

    import axios from 'axios';

    const api = axios.create({
        baseURL: "https://localhost:7241",
        headers: {'Accept': 'application/json',
                'Content-Type': 'application/json'}
    })    

    export default defineComponent({
        name: 'Products-Catalog',
        data() {
        return {
            page: 1,
            totpage: 0,
            totRecs: 0,
            catalogs: [],
            errors: [],            
        }
    },
    mounted() {
        this.getCatalogs(this.page);
    },
    methods: {
        getCatalogs: function(page: number) {
            api.get(`/api/listproducts/${page}`)
            .then((res) => {
                this.catalogs = res.data.products;
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
            this.getCatalogs(this.page);
            return;
        },
        prevPage: function(event: any) {
            event.preventDefault();    
            if (this.page == 1) {
            return;
            }
            this.page = this.page - 1;
            this.getCatalogs(this.page);
            return;    
        },
        firstPage: function(event: any) {
            event.preventDefault();    
            this.page = 1;
            this.getCatalogs(this.page);
            return;    
        },
        lastPage: function(event: any) {
            event.preventDefault();    
            this.page = this.totpage;
            this.getCatalogs(this.page);
            return;    

        }
    }        
})
</script>