<script setup lang="ts">
import { ref, reactive } from 'vue';
import { useRouter } from 'vue-router';
import type { FormInstance, FormRules } from 'element-plus';
import { AxiosError } from 'axios';
import { rawHttp as http } from '../services/http';

const router = useRouter();

interface LoginForm {
    login: string;
    password: string;
}

const loginForm = reactive<LoginForm>({
    login: '',
    password: '',
});

const loginFormRef = ref<FormInstance>();

const rules = reactive<FormRules<LoginForm>>({
    login: [
        { required: true, message: 'Please enter user login', trigger: 'blur', },
    ],
    password: [
        { required: true, message: 'Please enter password', trigger: 'blur' },
    ],
});

const onLogin = async (formElement: FormInstance | undefined) => {
    if (!formElement) return;
    if (!await formElement.validate()) return;
    try {
        const result = await http.post('account/login', loginForm);
        router.push({ name: 'hall', });
    }
    catch (error) {
        if (error instanceof AxiosError && error.response?.status === 400) {
            alert(error.response?.data.error);
        }
        console.log(error);
    }
};
</script>

<template>
    <el-container>
        <el-header>
            <h1>Login to Reservations</h1>
        </el-header>
        <el-main>
            <el-form :model="loginForm" ref="loginFormRef" @keyup.enter.native="onLogin(loginFormRef)" label-width="100px" :rules="rules">
                <el-form-item label="Login" prop="login">
                    <el-input v-model="loginForm.login" />
                </el-form-item>
                <el-form-item label="Password" prop="password">
                    <el-input v-model="loginForm.password" type="password" show-password />
                </el-form-item>
                <el-form-item>
                    <el-button type="primary" @click="onLogin(loginFormRef)">Login</el-button>
                </el-form-item>
            </el-form>
        </el-main>
    </el-container>
</template>

<style scoped>
section {
    margin: auto;
    width: 600px;
}

header h1 {
    text-align: center;
}
</style>
