<template>
	<div class="blog-tag-container layout-padding">
		<ProTable ref="tableRef" :request-api="getTableList" :data-callback="dataCallBack" :columns="columns" :tool-button="false">
			<!-- v-auth="'tags:add'" -->
			<template #tools> <el-button type="primary" icon="ele-Plus" @click="onOpen(null)"> 新增 </el-button></template>
			<template #status="scope">
				<el-tag :type="scope.row.status === 0 ? 'success' : 'danger'"> {{ scope.row.status === 0 ? '启用' : '禁用' }}</el-tag>
			</template>
			<template #cover="{ row }">
				<el-image shape="square" :size="100" fit="cover" :src="row.cover" />
			</template>
			<template #action="scope">
				<!-- v-auth="'tags:edit'"  -->
				<el-button icon="ele-Edit" size="small" text type="primary" @click="onOpen(scope.row)"> 编辑 </el-button>
				<el-popconfirm title="确认删除吗？" @confirm="onDeleteRole(scope.row.id)">
					<template #reference>
						<!-- v-auth="'tags:delete'" -->
						<el-button icon="ele-Delete" size="small" text type="danger"> 删除 </el-button>
					</template>
				</el-popconfirm>
			</template>
		</ProTable>
		<TagDialog ref="tagDialogRef" @refresh="tableRef?.reset" />
	</div>
</template>

<script setup lang="ts" name="blogTags">
import { defineAsyncComponent, inject, reactive, ref } from 'vue';
import { ElMessage } from 'element-plus';

// 引入组件
const TagDialog = defineAsyncComponent(() => import('./dialog.vue'));
import ProTable from '@/components/ProTable/index.vue';
import { ColumnProps } from '@/components/ProTable/interface';
import { CreateOrUpdateTagInput, TagsPageQueryInput, TagssServiceProxy } from '@/shared/service-proxies';
import moment from 'moment';
const _tagsService = new TagssServiceProxy(inject('$baseurl'), inject('$api'));
//  table实例
const tableRef = ref<InstanceType<typeof ProTable>>();
// 弹窗实例
const tagDialogRef = ref<InstanceType<typeof TagDialog>>();
const columns = reactive<ColumnProps[]>([
	{
		type: 'index',
		label: '序号',
		width: 60,
	},
	{
		prop: 'name',
		label: '标签名称',
		search: { el: 'input' },
		width: 200,
	},
	{
		prop: 'cover',
		label: '封面',
		width: 180,
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
		prop: 'createdTime',
		label: '创建时间',
	},
	{
		prop: 'action',
		label: '操作',
		fixed: 'right',
		width: 150,
		// isShow: auths(['tags:edit', 'tags:delete']),
	},
]);

const dataCallBack = (data) => {
	data.rows.forEach((res) => {
		if (res.createdTime) {
			res.createdTime = moment(res.createdTime).format('YYYY-MM-DD');
		}
	});
	return data;
};

const getTableList = (params: any) => {
	let newParams = JSON.parse(JSON.stringify(params)) as TagsPageQueryInput;
	return _tagsService.getPage(newParams);
};

// 打开新增标签弹窗
const onOpen = (row: CreateOrUpdateTagInput | null) => {
	tagDialogRef.value?.openDialog(row);
};

// 删除角色
const onDeleteRole = async (id: string) => {
	const { success } = await _tagsService.delete(id);
	if (success) {
		ElMessage.success('删除成功');
		tableRef.value?.reset();
	}
};
</script>
<style scoped></style>
