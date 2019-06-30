<style lang="less">
  @import './login.less';
</style>

<template>
  <div class="login">
    <div class="login-con">
      <Card icon="log-in" title="欢迎登录" :bordered="false">
        <div class="form-con">
          <login-form ref="login-form" @on-success-valid="handleSubmit"></login-form>
        </div>
      </Card>
    </div>
  </div>
</template>

<script>
import LoginForm from '_c/login-form'
import { mapActions } from 'vuex'
export default {
  components: {
    LoginForm
  },
  methods: {
    ...mapActions([
      'handleLogin',
      'getUserInfo'
    ]),
    handleSubmit ({ userName, password, code }) {
      this.handleLogin({ userName, password, code }).then(res => {
        console.log(res)
        if (!res.data.success) {
          this.$refs['login-form'].loadingFalse()
          this.$Message.error(res.data.msg)
          if (res.data.status !== 'WrongYzm') {
            this.$refs['login-form'].getValidateImage()
          }
          return
        }
        this.getUserInfo().then(res => {
          this.$router.push({
            name: this.$config.homeName
          })
        })
      }).catch(function (error) {
        console.log('er', error)
      })
    }
  }
}
</script>

<style>

</style>
