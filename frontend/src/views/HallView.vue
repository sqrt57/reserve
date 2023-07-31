<script setup lang="ts">
import { ref, watch } from 'vue';
import { useRoute, useRouter } from "vue-router";
import { logout } from '../dataServices/account';

const route = useRoute();
const router = useRouter();

const activeIndex = ref(route.name?.toString());
watch(
    () => route.name,
    async () => {
        activeIndex.value = route.name?.toString();
    }
);

const handleSelect = (index: string) => {
    if (index === "logout") {
        logout();
    } else {
        router.push({ name: index });
    }
}

</script>

<template>
    <header>
        <el-menu :default-active="activeIndex" mode="horizontal" @select="handleSelect" :ellipsis="false">
            <el-menu-item index="visitors">Visitors</el-menu-item>
            <el-menu-item index="visitors-history">Today</el-menu-item>
            <div class="flex-grow" />
            <el-menu-item index="logout">Logout</el-menu-item>
        </el-menu>
    </header>
    <main>
        <router-view />
    </main>
</template>

<style>
.flex-grow {
    flex-grow: 1;
}
</style>
