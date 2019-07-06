<template>
<div>
  <Card>
    <div class="table-search">
      <Form ref="formInline" :model="searchForm" inline>
        <FormItem>
          <Input clearable placeholder="输入角色名称搜索" class="search-input" v-model="searchForm.Name">
          <span slot="prepend">角色名称：</span>
          </Input>
        </FormItem>
        <FormItem>
          <Button type="primary" @click="getAllRole">搜索</Button>
        </FormItem>
        <FormItem>
          <Button type="success" @click="addRole">添加</Button>
        </FormItem>
      </Form>
    </div>
    <Table border stripe :columns="tableColumns" :data="tableData" :loading="tableLoading" :height="lheight"></Table>
    <div class="table-pagging">
      <Page :current="currentPage" :total="pageTotal" :page-size="pageSize" @on-change="pageChange" @on-page-size-change="pageSizeChange" show-total show-sizer/>
    </div>
  </Card>
  <RoleInfo :title="infoTitle" :r-id="infoUId" v-model="isShowInfo" @on-save="getAllRole"></RoleInfo>
  <RoleMenu :title="infoTitle" :r-id="infoUId" v-model="isShowMenu" @on-save="getAllRole"></RoleMenu>
</div>
</template>

<script>
import { getRolePagging, deleteRole } from '@/api/system'
import RoleInfo from './role-info.vue'
import RoleMenu from './role-menu.vue'
export default {
  components: {
    RoleInfo,
    RoleMenu
  },
  data () {
    return {
      tableLoading: false,
      currentPage: 1,
      pageTotal: 0,
      pageSize: 20,
      tableColumns: [
        {
          title: '序号',
          type: 'index',
          fixed: 'left',
          align: 'center',
          width: 80
        },
        {
          title: '角色名称',
          key: 'Name',
          align: 'center',
          minWidth: 150
        },
        {
          title: '描述',
          key: 'Description',
          align: 'center',
          minWidth: 130
        },
        {
          title: '操作',
          align: 'center',
          width: 260,
          render: (h, params) => {
            return h('div', [
              h('Button', {
                props: {
                  type: 'primary',
                  loading: params.row.btnLoading
                },
                style: {
                  marginRight: '5px'
                },
                on: {
                  click: (e) => {
                    this.updateRole(params.row.Id)
                  }
                }
              }, '修改'),
              h('Button', {
                props: {
                  type: 'info',
                  loading: params.row.btnLoading
                },
                style: {
                  marginRight: '5px'
                },
                on: {
                  click: (e) => {
                    this.configMenu(params.row.Id)
                  }
                }
              }, '分配权限'),
              h('Poptip', {
                props: {
                  confirm: true,
                  title: '您确定要删除吗?'
                },
                on: {
                  'on-ok': (e) => {
                    this.deleteRole(params.row.Id)
                  }
                }
              }, [
                h('Button', {
                  props: {
                    type: 'error'
                  }
                }, '删除')
              ])
            ])
          }
        }
      ],
      tableData: [],
      roleList: [],
      infoTitle: '',
      infoUId: 0,
      isShowInfo: false,
      isShowMenu: false,
      searchForm: {
        RoleCode: '',
        UserName: ''
      },
      lheight: 0
    }
  },

  methods: {
    getAllRole () {
      getRolePagging(this.currentPage, this.pageSize, this.searchForm).then(res => {
        if (res.data.IsSuccess) {
          this.tableData = res.data.Data.Data
          this.currentPage = res.data.Data.CurrentPage
          this.pageTotal = res.data.Data.Total
          this.tableLoading = false
        }
      })
    },
    pageChange (page) {
      this.currentPage = page
      this.getAllRole()
    },
    pageSizeChange (pageSize) {
      this.pageSize = pageSize
      this.currentPage = 1
      this.getAllRole()
    },
    configMenu (id) {
      this.infoTitle = '访问配置'
      this.infoUId = id
      this.isShowMenu = true
    },
    addRole () {
      this.infoUId = '0'
      this.isShowInfo = true
      this.infoTitle = '添加角色'
    },
    deleteRole (roleId) {
      this.tableLoading = true
      deleteRole(roleId).then(res => {
        if (res.data.IsSuccess) {
          this.$Message.success(res.data.msg)
        } else {
          this.$Message.error(res.data.msg)
        }
        this.tableLoading = false
        this.getAllRole()
      })
    },
    updateRole (userId) {
      this.infoTitle = '修改角色'
      this.infoUId = userId
      this.isShowInfo = true
    }
  },
  mounted () {
    this.getAllRole()
    var that = this
    that.lheight = document.documentElement.clientHeight - 315
    window.onresize = function () {
      that.lheight = document.documentElement.clientHeight - 315
    }
  }
}
</script>
