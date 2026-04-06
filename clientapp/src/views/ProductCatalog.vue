<script setup lang="ts">
import { ref, onMounted } from 'vue';
import axios, { AxiosError } from 'axios';

interface ApiError {
   message: string;
 } 

interface Product {
    id: number;
    productPicture: string;
    descriptions: string;
    sellPrice: number;
}

interface ApiResponse {
    products: Product[];
    totpage: number;
    page: number;
}

const page = ref(1);
const message = ref('');
const totpage = ref(0);
const catalogs = ref<Product[]>([]);

const api = axios.create({
    baseURL: "https://localhost:7241",
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
    }
});


const getCatalogs = async (targetPage: number) => {
    try {
        const res = await api.get<ApiResponse>(`/api/listproducts/${targetPage}`);
        catalogs.value = res.data.products;
        totpage.value = res.data.totpage;
        page.value = res.data.page;
    } catch (err) { // <--- Changed from: }, (error: AxiosError<ApiError>) => {
        const error = err as AxiosError<ApiError>; // Cast error for TypeScript
        
        if (error.response) {
            message.value = error.response.data.message || 'Server Error';
        } else {
            message.value = error.message;
        }
        
        setTimeout(() => {
            window.location.reload();
        }, 3000);
    }
};

// Pagination Logic
const nextPage = (event: Event) => {
    event.preventDefault();
    if (page.value < totpage.value) {
        page.value++;
        getCatalogs(page.value);
    }
};

const prevPage = (event: Event) => {
    event.preventDefault();
    if (page.value > 1) {
        page.value--;
        getCatalogs(page.value);
    }
};

const firstPage = (event: Event) => {
    event.preventDefault();
    page.value = 1;
    getCatalogs(1);
};

const lastPage = (event: Event) => {
    event.preventDefault();
    page.value = totpage.value;
    getCatalogs(totpage.value);
};

onMounted(() => {
    getCatalogs(page.value);
});
</script>

<style scoped>
    .card-size {
        width: 300px!important;
    }
    .product-size {
        width: 240px!important;
        height: 340px!important;
    }
    .price-size {
        width: 215px;
    }
    .hdr {
        font-size: 24px;
        font-weight: bold;
        text-align: center;
    }
</style>

<template>
    <div class="container-fluid mt-4">
        <div class="card-header bg-light rounded hdr">
            Product Catalogs
        </div>        
        <div v-if="message" class="text-danger">{{ message }}</div>

        <div class="card-group">
            <div v-for="prod in catalogs" :key="prod.id" class="card">
                <img :src="prod.productPicture" class="card-img-top product-size" alt="Product Image"/>
                <div class="card-body">
                    <h5 class="card-title">Description</h5>
                    <p class="card-text">{{ prod.descriptions }}</p>
                </div>
                <div class="card-footer">
                    <!-- FIXED: Added optional chaining or default 0 to ensure toFixed(2) doesn't crash if sellPrice is null -->
                    <p class="card-text text-danger price-size">
                        <span class="text-dark">PRICE :</span>&nbsp;
                        <strong>₱{{ (prod.sellPrice ?? 0).toFixed(2) }}</strong>
                    </p>
                </div>  
            </div>
        </div>    

        <nav aria-label="Page navigation example">
            <ul class="pagination mt-4">
                <li class="page-item"><a @click="lastPage" class="page-link" href="#">Last</a></li>
                <li class="page-item"><a @click="prevPage" class="page-link" href="#">Previous</a></li>
                <li class="page-item"><a @click="nextPage" class="page-link" href="#">Next</a></li>
                <li class="page-item"><a @click="firstPage" class="page-link" href="#">First</a></li>
                <li class="page-item page-link text-danger">Page&nbsp;{{ page }} of&nbsp;{{ totpage }}</li>
            </ul>
        </nav>
    </div>
</template>