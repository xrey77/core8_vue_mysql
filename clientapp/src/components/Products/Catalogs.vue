<template>
<div class="container-fluid mb-4">
    <h4 class="text-center">Product Catalogs</h4>
    <div class="card-group">
        <div v-for="prod in catalogs" :key="prod.id" class="card card-size">
            <img v-bind:src="prod.productPicture" class="card-img-top product-size" alt=""/>
            <div class="card-body">
                <h5 class="card-title">Descriptions</h5>
                <p class="card-text">{{prod.descriptions}}</p>
            </div>
            <div class="card-footer">
                <p class="card-text text-danger price-size"><span class="text-dark">PRICE :</span>&nbsp;<strong>&#8369;{{prod.sellPrice.toFixed(2)}}</strong></p>
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

<style scoped>

.card-size {
    width: 300px!important;
}
.product-size {
    width: 240px!important;
    height: 280px!important;
}
.price-size {
    width: 215px;
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
        name: 'Catalogs-Page',
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
        /* eslint-disable */ 
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