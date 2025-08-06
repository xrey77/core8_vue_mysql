<template>
<div class="container mb-4">
    <h2>Search Product</h2>

    <form class="row g-3" @submit.prevent="getProdsearch" autocomplete="off">
        <div class="col-auto">
          <input type="text" required class="form-control-sm" v-model="search" name="search" placeholder="enter Product keyword">
        </div>
        <div class="col-auto">
          <button type="submit" class="btn btn-primary btn-sm mb-3">search</button>
        </div>
    </form>
    
    <div class="card-group mb-4">

      <div class="card" v-for="product in prodsearch" :key="product.id">
        <img v-bind:src="product.prod_pic" class="card-img-top product-size" alt="...">
        <div class="card-body">
          <h5 class="card-title">Descriptions</h5>
          <p class="card-text">{{product.descriptions}}</p>
      </div>
      <div class="card-footer">
          <p class="card-text text-danger"><span class="text-dark">PRICE :</span>&nbsp;<strong>
            &#8369;{{product.sell_price}}
        </strong></p>
      </div>  

      </div>
  
  </div>    
  

</div>
</template>

<style scoped>

.product-size {
    width: 300px;
    height: auto;
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
        name: 'Search-Product',
        data() {
            return {
                search: null,
                prodsearch: [],
                errors: [],            
            }
        },
        mounted() {
        },
        methods: {
            getProdsearch: function() {
                const data = JSON.stringify({ search: this.search});

                api.post("/api/searchproducts",data)
                .then((res) => {
                    this.prodsearch = res.data.products;
                }, (error) => {
                        console.log(error.message);
                        return;
                });
            },
            submitSearchForm: function() {

            },
        },   
})
</script>