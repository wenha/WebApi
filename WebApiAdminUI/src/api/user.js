import axios from '@/libs/api.request'

// 登录
export const login = ({ userName, password, code }) => {
  const data = {
    LoginName: userName,
    Password: password,
    Code: code
  }
  return axios.request({
    url: '/Account/Login',
    data,
    method: 'post'
  })
}

// 获取用户信息
export const getUserInfo = () => {
  return axios.request({
    url: '/Account/Info',
    method: 'get'
  })
}

// 退出
export const logout = () => {
  return axios.request({
    url: '/Account/LoginOut',
    method: 'get'
  })
}
