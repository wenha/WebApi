<template>
    <Modal v-model="showInfo" :mask-closable="false" :title="title" :loading="loading" @on-ok="save" @on-cancel='cancel' class="etl-user-body">
        <div class="spin-article">
            <Form ref="infoForm" :model="infoForm" :rules='ruleInfo' :label-width="100">
                <FormItem label="角色名称" style="width:400px" prop='Name'>
                  <Input v-model="infoForm.Name" placeholder="请填写角色名称" />
                </FormItem>
                <FormItem label="角色描述" style="width:400px" prop='Description'>
                  <Input v-model="infoForm.Description" placeholder="请填写角色描述" />
                </FormItem>
            </Form>
            <Spin size="large" fix v-if="spinShow">加载中...</Spin>
        </div>
    </Modal>
</template>
<script>
import { getRoleInfo, saveRole } from '@/api/system'
export default {
  props: ['rId', 'title', 'value'],
  data () {
    return {
      loading: true,
      spinShow: false,
      infoForm: {
        Id: this.rId,
        Name: '',
        Description: ''
      },
      ruleInfo: {
        Name: [
          { required: true, message: '请填写角色名称', trigger: 'change' }
        ]
      }
    }
  },
  watch: {
    value: function (newVal, oldVal) {
      if (newVal) {
        this.show()
      }
    }
  },
  computed: {
    showInfo: {
      get () {
        return this.value
      },
      set (val) {
        this.$emit('input', val)
      }
    }
  },
  methods: {
    show () {
      let _this = this
      if (this.value) { // 显示对话框
        _this.infoForm = {
          Id: this.rId,
          Name: '',
          Description: ''
        }
        if (this.rId !== '0') {
          this.getRoleInfo()
        }
      }
    },
    getRoleInfo () {
      getRoleInfo(this.rId).then(res => {
        if (res.data.IsSuccess) {
          this.infoForm = res.data.Data
        }
      })
    },
    save () {
      this.$refs['infoForm'].validate((valid) => {
        if (!valid) {
          this.loading = false
          this.$nextTick(() => {
            this.loading = true
          })
          return
        }
        saveRole(this.infoForm).then(res => {
          debugger
          if (res.data.IsSuccess) {
            this.showInfo = false
            this.$Message.success(res.data.Msg)
            this.infoForm = {
              Id: this.rId,
              Name: '',
              Description: ''
            }
            this.$emit('on-save')
          } else {
            this.$Message.error(res.data.msg)
            this.loading = false
            this.$nextTick(() => {
              this.loading = true
            })
          }
        })
      })
    },
    cancel () {
      this.showInfo = false
      this.$emit('on-save')
    }
  },
  mounted () {
  }
}
</script>
