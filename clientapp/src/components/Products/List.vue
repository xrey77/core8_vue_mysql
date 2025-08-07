<template>
  <div class="container-fluid">
    <h1 class="text-center">Product List</h1>

    <table class="table">
        <thead>
          <tr>
            <th scope="col">#</th>
            <th scope="col">Descriptions</th>
            <th scope="col">Stocks</th>
            <th scope="col">Unit</th>
            <th scope="col">Price</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in prods" :key="item.id">
            <td>{{ item.id }}</td>
            <td>{{ item.descriptions }}</td>
            <td>{{ item.qty }}</td>
            <td>{{ item.unit }}</td>
            <td>&#8369;{{ item.sellPrice.toFixed(2) }}</td>
          </tr>
        </tbody>
    </table>    
    <!-- return new Intl.NumberFormat('de-DE', { minimumFractionDigits: 2, maximumFractionDigits: 2 }).format(rawNumber.value); -->
    <nav aria-label="Page navigation example">
        <ul class="pagination">
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
    name: 'List-Page',
    data() {
        return {
            page: 1,
            totpage: 0,
            totRecs: 0,
            prods: [],
            errors: [],            
        }
    },
    mounted() {
        /* eslint-disable */ 
        this.getProducts(this.page);
    },
    methods: {        
        getProducts: function(page: number) {
            api.get(`/api/listproducts/${page}`)
            .then((res) => {                
                this.prods = res.data.products;
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
            this.getProducts(this.page);
            return;
        },
        prevPage: function(event: any) {
            event.preventDefault();    
            if (this.page == 1) {
            return;
            }
            this.page = this.page - 1;
            this.getProducts(this.page);
            return;    
        },
        firstPage: function(event: any) {
            event.preventDefault();    
            this.page = 1;
            this.getProducts(this.page);
            return;    
        },
        lastPage: function(event: any) {
            event.preventDefault();    
            this.page = this.totpage;
            this.getProducts(this.page);
            return;    

        }
    }
})

</script>