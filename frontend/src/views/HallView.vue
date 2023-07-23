<script setup lang="ts">
</script>

<template>
    <nav>
        <router-link :to="{ name: 'visitors' }">Visitors</router-link>
        <router-link :to="{ name: 'goods-and-services' }">Goods and Services</router-link>
    </nav>
    <main>
        <router-view />
    </main>
</template>
