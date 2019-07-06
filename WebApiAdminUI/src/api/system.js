import axios from '@/libs/api.request'

// 获取角色分页
export const getRolePagging = (page, pageSize, data) => {
  return axios.request({
    url: '/Role/GetRolePagging',
    params: {
      page,
      pageSize
    },
    data,
    method: 'post'
  })
}

// 获取单个角色
export const getRoleInfo = (roleId) => {
  return axios.request({
    url: '/Role/GetRoleById',
    params: {
      roleId
    },
    method: 'get'
  })
}

// 保存角色
export const saveRole = (data) => {
  return axios.request({
    url: '/Role/SaveRole',
    data,
    method: 'post'
  })
}

// 删除角色
export const deleteRole = (roleId) => {
  return axios.request({
    url: '/Role/DeleteRole',
    params: {
      roleId
    },
    method: 'get'
  })
}

// 获取树形菜单
export const getTreeMenus = (roleId) => {
  return axios.request({
    url: '/Role/GetTreeMenus',
    params: {
      roleId
    },
    method: 'get'
  })
}

// 获取树形菜单
export const saveTreeMenus = (roleId, ids) => {
  return axios.request({
    url: '/Role/SaveTreeMenu',
    params: {
      roleId,
      ids
    },
    method: 'get'
  })
}
