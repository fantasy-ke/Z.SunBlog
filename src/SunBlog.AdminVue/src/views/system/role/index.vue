<template>
	<div class="system-role-container layout-padding">
		<ProTable ref="tableRef" :request-api="getTableList" :columns="columns" :tool-button="false">
			<!-- v-auth="'sysrole:add'" -->
			<template #tools> <el-button type="primary" icon="ele-Plus" @click="onOpenRole(null)"> 新增 </el-button></template>
			<template #status="scope">
				<el-tag :type="scope.row.status === 0 ? 'success' : 'danger'"> {{ scope.row.status === 0 ? '启用' : '禁用' }}</el-tag>
			</template>
			<template #action="scope">
				<!-- v-auth="'sysrole:edit'" -->
				<el-button icon="ele-Edit" size="small" text type="primary" @click="onOpenRole(scope.row)"> 编辑 </el-button>
				<el-popconfirm title="确认删除吗？" @confirm="onDeleteRole(scope.row.id)">
					<template #reference>
						<!-- v-auth="'sysrole:delete'" -->
						<el-button icon="ele-Delete" size="small" text type="danger"> 删除 </el-button>
					</template>
				</el-popconfirm>
			</template>
		</ProTable>
		<RoleDialog ref="roleDialogRef" @refresh="tableRef?.reset" />
	</div>
</template>

<script setup lang="ts" name="sysRole">
import { defineAsyncComponent, inject, reactive, ref } from 'vue';
import { ElMessage } from 'element-plus';
import { auths } from '@/utils/authFunction';

// 引入组件
const RoleDialog = defineAsyncComponent(() => import('@/views/system/role/dialog.vue'));
import ProTable from '@/components/ProTable/index.vue';
import { ColumnProps } from '@/components/ProTable/interface';
import { RoleQueryInput, RoleSyssServiceProxy, UpdateSysRoleInput } from '@/shared/service-proxies';
const _roleSysService = new RoleSyssServiceProxy(inject('$baseurl'), inject('$api'));
//  table实例
const tableRef = ref<InstanceType<typeof ProTable>>();
// 表单实例
const roleDialogRef = ref<InstanceType<typeof RoleDialog>>();
const columns = reactive<ColumnProps[]>([
	{
		type: 'index',
		label: '序号',
		width: 60,
	},
	{
		prop: 'name',
		label: '角色名称',
		search: { el: 'input' },
		width: 200,
	},
	{
		prop: 'code',
		label: '角色标识',
	},
	{
		prop: 'sort',
		label: '排序',
	},
	{
		prop: 'status',
		label: '状态',
	},
	{
		prop: 'creationTime',
		label: '创建时间',
	},
	{
		prop: 'action',
		label: '操作',
		align: 'center',
		fixed: 'right',
		width: 150,
		// isShow: auths(['sysrole:edit', 'sysrole:delete']),
	},
]);
// 打开新增角色弹窗
const onOpenRole = (row: UpdateSysRoleInput | null) => {
	roleDialogRef.value?.openDialog(row);
};

const getTableList = (params: any) => {
	let newParams = JSON.parse(JSON.stringify(params)) as RoleQueryInput;
	return _roleSysService.getPage(newParams);
};

// 删除角色
const onDeleteRole = async (id: string) => {
	const { success } = await _roleSysService.delete(id);
	if (success) {
		ElMessage.success('删除成功');
		tableRef.value?.reset();
	}
};
</script>
<style scoped lang="scss"></style>
