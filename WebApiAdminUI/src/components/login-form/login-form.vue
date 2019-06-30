<template>
  <Form ref="loginForm" :model="form" :rules="rules" @keydown.enter.native="handleSubmit">
    <FormItem prop="userName">
      <Input v-model="form.userName" placeholder="请输入用户名">
        <span slot="prepend">
          <Icon :size="16" type="md-person"></Icon>
        </span>
      </Input>
    </FormItem>
    <FormItem prop="password">
      <Input type="password" v-model="form.password" placeholder="请输入密码">
        <span slot="prepend">
          <Icon :size="16" type="md-lock"></Icon>
        </span>
      </Input>
    </FormItem>
    <FormItem prop="code" class="etl-val-code" v-if="showCode">
      <Input v-model="form.code" placeholder="请输入验证码">
        <span slot="prepend">
          <Icon :size="16" type="md-images"></Icon>
        </span>
      </Input>
      <img :src="img" @click="getValidateImage"/>
    </FormItem>
    <FormItem>
      <Button @click="handleSubmit" :loading ="loading" type="primary" long>登录</Button>
    </FormItem>
  </Form>
</template>
<script>
import './login-form.less'
import {
  validateImage
} from '@/api/home'
export default {
  name: 'LoginForm',
  props: {
    userNameRules: {
      type: Array,
      default: () => {
        return [
          { required: true, message: '账号不能为空', trigger: 'blur' }
        ]
      }
    },
    passwordRules: {
      type: Array,
      default: () => {
        return [
          { required: true, message: '密码不能为空', trigger: 'blur' }
        ]
      }
    }
  },
  data () {
    return {
      img: '',
      loading: false,
      showCode: false,
      form: {
        userName: '',
        password: '',
        code: ''
      }
    }
  },
  computed: {
    rules () {
      return {
        userName: this.userNameRules,
        password: this.passwordRules,
        code: { required: true, message: '验证码不能为空', trigger: 'blur' }
      }
    }
  },
  methods: {
    getValidateImage () {
      this.img = validateImage + '?_=' + new Date().getTime()
      this.form.code = ''
    },
    loadingFalse () {
      this.loading = false
    },
    handleSubmit () {
      this.$refs.loginForm.validate((valid) => {
        if (valid) {
          this.loading = true
          this.$emit('on-success-valid', {
            userName: this.form.userName,
            password: this.form.password,
            code: this.form.code
          })
        }
      })
    }
  },
  mounted () {
    this.getValidateImage()
  }
}
</script>
