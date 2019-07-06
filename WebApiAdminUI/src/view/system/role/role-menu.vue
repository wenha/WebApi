<template>
    <Modal v-model="isShowConfig" :mask-closable="false" :title="title" :loading="loading" @on-ok="save" :footer-hide="spinShow" scrollable>
        <div class="spin-article">
            <Tree :data="tdata" show-checkbox ref='tree' style="left:70px;position:relative"></Tree>
            <Spin size="large" fix v-if="spinShow">加载中...</Spin>
        </div>
    </Modal>
</template>
<script>
import { getTreeMenus, saveTreeMenus } from '@/api/system'
export default {
  props: ['rId', 'title', 'value'],
  data () {
    return {
      loading: true,
      spinShow: false,
      tdata: [{
        Id: this.rId,
        title: '',
        expand: true,
        disabled: false,
        disableCheckbox: false,
        selected: false,
        chceked: false,
        children: []
      }]
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
    isShowConfig: {
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
      _this.spinShow = this.rId !== '0'
      if (this.value) { // 显示对话框
        _this.tdata = [{
          Id: this.rId,
          title: '',
          expand: true,
          disabled: false,
          disableCheckbox: false,
          selected: false,
          chceked: false,
          children: []
        }]
        _this.getTreeMenu()
      }
    },
    getTreeMenu () {
      if (this.rId !== undefined && this.rId !== 0) {
        let that = this
        that.loading1 = true
        getTreeMenus(this.rId).then(res => {
          if (res.data.IsSuccess) {
            that.tdata = res.data.Data
            that.loading1 = false
            this.spinShow = false
          }
        })
      }
    },
    save () {
      // 获取所有选中节点
      let params = this.$refs.tree.getCheckedNodes()
      // 所有数据
      let allData = this.tdata
      // 循环执行所有选中的节点链，放到arr1数组里
      let arr1 = []
      for (let i = 0; i < params.length; i++) {
        // 单条数据链
        let aData = this.getParent(allData, [], params[i].id)
        for (let y = 0; y < aData.length; y++) {
          // 拆分成单个json数组放到arr1里
          arr1.push(aData[y])
        }
      }

      // arr1去重 es6的set方法
      function dedupe (array) {
        return Array.from(new Set(array))
      }

      arr1 = dedupe(arr1)

      var ids = ''
      for (var index in arr1) {
        ids += arr1[index].id + ','
      }

      saveTreeMenus(this.rId, ids).then(res => {
        if (res.data.IsSuccess) {
          this.isShowConfig = false
          this.$Message.success(res.msg)
          this.tdata = [{
            Id: this.rId,
            title: '',
            expand: true,
            disabled: false,
            disableCheckbox: false,
            selected: false,
            chceked: false,
            children: []
          }]
          this.$emit('on-save')
        } else {
          this.$Message.error(res.msg)
          this.loading = false
          this.$nextTick(() => {
            this.loading = true
          })
        }
      })
    },

    // 获取整条数据链
    getParent (array, childs, ids) {
      for (let i = 0; i < array.length; i++) {
        let item = array[i]
        if (Number(item.id) === Number(ids)) {
          childs.push(item)
          return childs
        }
        if (item.children && item.children.length > 0) {
          childs.push(item)
          let rs = this.getParent(item.children, childs, ids)
          if (rs) {
            return rs
          } else {
            childs.splice(childs.indexOf(item))
          }
        }
      }
      return false
    }
  },
  mounted () {
    this.getTreeMenu()
  }
}
</script>
